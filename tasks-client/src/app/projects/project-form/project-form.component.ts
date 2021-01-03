import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectService } from 'src/app/shared/services/project.service';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { ProjectDetailDto } from '../dtos/project-detail.dto';
import { v4 as uuidv4 } from 'uuid';
import { ProjectCreateDto } from '../dtos/project-create.dto';
import { ProjectUpdateDto } from '../dtos/project-update.dto';
import { HttpErrorResponse } from '@angular/common/http';
import { Status } from 'src/app/shared/models/status.enum';
import { Result } from 'src/app/shared/models/result';
import { Observable } from 'rxjs';
import { DeveloperListDto } from 'src/app/developers/dtos/developer-list.dto';
import { DeveloperService } from 'src/app/shared/services/developer.service';
import { Pagination } from 'src/app/shared/models/pagination';
import { map, startWith } from 'rxjs/operators';
import * as _ from 'lodash';
import { MatChipInputEvent } from '@angular/material/chips';

@Component({
  selector: 'app-project-form',
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.scss']
})
export class ProjectFormComponent implements OnInit {

  form: FormGroup;
  isNew: boolean;
  id: string | null;
  project?: ProjectDetailDto | null;
  saving: boolean = false;
  
  @ViewChild('developersInput') developerIdsInput: ElementRef;
  developers: DeveloperListDto[];
  filteredDevelopers: Observable<DeveloperListDto[]>;
  selectedDevelopers: DeveloperListDto[] = [];

  constructor(
    private readonly router: Router,
    private readonly activatedRoute: ActivatedRoute,
    private readonly developerService: DeveloperService,
    private readonly projectService: ProjectService,
    private readonly snackBar: MatSnackBar,
    private readonly fb: FormBuilder
  ) { 
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.isNew = !this.id;
    this.form = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(150)]],
      developerIds: [[]],
      description: ['', [Validators.maxLength(500)]]
    });
    
    if (this.id) this.loadProject(this.id);
    this.loadDevelopers();
  }

  ngOnInit(): void { }

  loadProject(id: string) {
    this.form.removeControl('cpf');
    this.form.removeControl('password');
    this.projectService.get(id).subscribe(result => {
      this.project = result.data;
      this.selectedDevelopers = this.project.developers;
      this.form.patchValue(this.project);
      this.form.controls.developerIds.setValue('');
    }, () => {
      this.snackBar.open('Falha ao carregar projeto!', 'OK', { duration: 3000 });
    });
  }

  loadDevelopers() {
    this.developerService.list({ page: 1, limit: 100 } as Pagination).subscribe(result => {
      this.developers = result.data;
      this.filterDevelopers();
    });
  }

  save() {
    if (this.saving || !this.valid()) return;

    this.saving = true;
    const project = this.form.getRawValue();
    this.id = project.id = this.id || uuidv4();
    project.developerIds = this.selectedDevelopers.map(d => d.id);
    const observable = this.isNew 
      ? this.projectService.create(project as ProjectCreateDto)
      : this.projectService.update(project as ProjectUpdateDto);
    observable.subscribe(result => {
      this.saving = false;
      if (!result.success) {
        this.snackBar.open('Falha ao salvar projeto', 'OK', { duration: 3000 });
        return;
      }

      this.snackBar.open('Projeto salvo com sucesso', 'OK', { duration: 3000 });
      this.close();
    }, (error: HttpErrorResponse) => {
      this.saving = false;
      const result = error.error as Result;
      if (error.status === Status.Conflict) {
        const existsTitle = 'Project with Title already exist';
        if (result.errorMessages.some(m => m.search(existsTitle) !== -1)) {
          this.snackBar.open('Já existe um projeto com este título cadastrado!', 'OK', { duration: 3000 });
          return;
        }
      }

      if (error.status === Status.Invalid) {
        this.snackBar.open('Existem campos inválidos!', 'OK', { duration: 3000 });
        return;
      }

      this.snackBar.open('Falha ao salvar projeto', 'OK', { duration: 3000 });
    });
  }

  valid(): boolean {
    this.form.markAllAsTouched();
    if (this.form.invalid) {
      this.snackBar.open('Existem campos inválidos!', 'OK', { duration: 3000 });
      return false;
    }

    return true;
  }

  close() {
    this.router.navigate(['projects']);
  }

  filterDevelopers() {
    this.filteredDevelopers = this.form.controls.developerIds.valueChanges
    .pipe(
      startWith<string | DeveloperListDto>(''),
      map(value => typeof value === 'string' ? value : value.name),
      map(name => {
        if (typeof name !== 'string') return [];
        const unselectedDevelopers = this.developers.filter(d => !this.selectedDevelopers.some(sd => sd.id === d.id));
        if (!name) return unselectedDevelopers.slice();
        const search = _.deburr(name).toLowerCase();
        return unselectedDevelopers.filter(type => _.deburr(type.name).toLowerCase().indexOf(search) !== -1);
      })
    );
  }

  displayDeveloper(value?: any): string {
    if (!value) return '';
    return typeof value === 'object' ? value.name : value;
  }

  selectDeveloper(event: MatAutocompleteSelectedEvent): void {
    this.selectedDevelopers.push(event.option.value as DeveloperListDto);
    this.developerIdsInput.nativeElement.value = '';
    this.form.controls.developerIds.setValue('');
    this.form.markAsDirty();
  }
  
  pushDeveloper(event: MatChipInputEvent): void {
    const { input, value } = event;
    const search = (value || '').trim();
    if (search) {
      const developer = this.developers.find(d => d.name === search);
      if (developer) {
        this.selectedDevelopers.push(developer);
        this.form.controls.developerIds.setValue('');
        this.form.markAsDirty();
      }
    }

    if (input) input.value = '';
  }

  removeDeveloper(developer: DeveloperListDto): void {
    this.selectedDevelopers = this.selectedDevelopers.filter(d => d.id !== developer.id);
    this.form.controls.developerIds.setValue('');
    this.form.markAsDirty();
  }
}
