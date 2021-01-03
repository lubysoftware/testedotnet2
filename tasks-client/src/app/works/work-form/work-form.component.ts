import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectListDto } from 'src/app/projects/dtos/project-list.dto';
import { ProjectWorkDetailDto } from 'src/app/projects/dtos/works/project-work-detail.dto';
import { Result } from 'src/app/shared/models/result';
import { Status } from 'src/app/shared/models/status.enum';
import { AuthService } from 'src/app/shared/services/auth.service';
import { ProjectService } from 'src/app/shared/services/project.service';
import { v4 as uuidv4 } from 'uuid';
import { WorkDto } from '../dtos/work.dto';

@Component({
  selector: 'app-work-form',
  templateUrl: './work-form.component.html',
  styleUrls: ['./work-form.component.scss']
})
export class WorkFormComponent implements OnInit {

  form: FormGroup;
  isNew: boolean;
  id: string | null;
  projectId: string | null;
  work?: ProjectWorkDetailDto | null;
  saving: boolean = false;
  currentDeveloperId: string;

  projects: ProjectListDto[] = [];

  constructor(
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly projectService: ProjectService,
    private readonly snackBar: MatSnackBar,
    private readonly fb: FormBuilder
  ) { 
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.projectId = this.activatedRoute.snapshot.paramMap.get('projectId');
    this.currentDeveloperId = this.authService.getCurrentDeveloper()?.id ?? '';
    this.isNew = !this.id;
    this.form = this.fb.group({
      projectId: [null, [Validators.required]],
      hours: [null, [Validators.required, Validators.min(0.1), Validators.max(3000)]],
      startTime: [null, [Validators.required]],
      endTime: [null, [Validators.required]],
      comment: [null, [Validators.required, Validators.maxLength(300)]],
    });
    
    if (this.id && this.projectId) 
      this.loadWork(this.id, this.projectId);
    else
      this.loadProjects();
  }

  ngOnInit(): void { }

  loadWork(id: string, projectId: string) {
    this.form.removeControl('projectId');
    this.projectService.getWork(id, projectId).subscribe(result => {
      if (result.data.developer.id !== this.currentDeveloperId) {
        this.close();
        return;
      }
      this.work = result.data;
      this.form.patchValue(this.work);
    }, () => {
      this.snackBar.open('Falha ao carregar atividade!', 'OK', { duration: 3000 });
    });
  }

  loadProjects() {
    this.projectService.list({ page: 1, limit: 100 }).subscribe(result => {
      this.projects = result.data;
    });
  }

  save() {
    if (this.saving || !this.valid()) return;

    this.saving = true;
    const work = this.form.getRawValue() as WorkDto;
    this.id = work.id = this.id || uuidv4();
    const observable = this.isNew 
      ? this.projectService.createWork(work)
      : this.projectService.updateWork(work);
    observable.subscribe(result => {
      this.saving = false;
      if (!result.success) {
        this.snackBar.open('Falha ao salvar atividade', 'OK', { duration: 3000 });
        return;
      }

      this.snackBar.open('Atividade salva com sucesso', 'OK', { duration: 3000 });
      this.close();
    }, (error: HttpErrorResponse) => {
      this.saving = false;
      const result = error.error as Result;
      if (error.status === Status.NotAllowed) {
        const notVinculated = 'Developer is not vinculated in Project';
        if (result.errorMessages.some(m => m.search(notVinculated) !== -1)) {
          this.snackBar.open('Você não está vinculado ao projeto!', 'OK', { duration: 3000 });
          return;
        }
      }

      if (error.status === Status.Invalid) {
        this.snackBar.open('Existem campos inválidos!', 'OK', { duration: 3000 });
        return;
      }

      if (error.status === Status.Error) {
        this.snackBar.open('Falha ao enviar notificação!', 'OK', { duration: 3000 });
        return;
      }

      this.snackBar.open('Falha ao salvar atividade', 'OK', { duration: 3000 });
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
    this.router.navigate(['works']);
  }
}
