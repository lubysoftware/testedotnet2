import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { DeveloperService } from 'src/app/shared/services/developer.service';
import { DeveloperDetailDto } from '../dtos/developer-detail.dto';
import { v4 as uuidv4 } from 'uuid';
import { DeveloperCreateDto } from '../dtos/developer-create.dto';
import { DeveloperUpdateDto } from '../dtos/developer-update.dto';
import { HttpErrorResponse } from '@angular/common/http';
import { Status } from 'src/app/shared/models/status.enum';
import { Result } from 'src/app/shared/models/result';

@Component({
  selector: 'app-developer-form',
  templateUrl: './developer-form.component.html',
  styleUrls: ['./developer-form.component.scss']
})
export class DeveloperFormComponent implements OnInit {

  form: FormGroup;
  isNew: boolean;
  id: string | null;
  developer?: DeveloperDetailDto | null;
  saving: boolean = false;

  constructor(
    private readonly router: Router,
    private readonly activatedRoute: ActivatedRoute,
    private readonly developerService: DeveloperService,
    private readonly snackBar: MatSnackBar,
    private readonly fb: FormBuilder
  ) {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.isNew = !this.id;
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(150)]],
      login: ['', [Validators.required, Validators.maxLength(150)]],
      cpf: ['', [Validators.required, Validators.maxLength(11), Validators.pattern(/^\d{11}$/)]],
      password: ['', [Validators.required, Validators.maxLength(50)]],
    });
    
    if (this.id) this.loadDeveloper(this.id);
  }

  ngOnInit(): void {}

  loadDeveloper(id: string) {
    this.form.removeControl('cpf');
    this.form.removeControl('password');
    this.developerService.get(id).subscribe(result => {
      this.developer = result.data;
      this.form.patchValue(this.developer);
    }, () => {
      this.snackBar.open('Falha ao carregar desenvolvedor!', 'OK', { duration: 3000 });
    });
  }

  save() {
    if (this.saving || !this.valid()) return;

    this.saving = true;
    const developer = this.form.getRawValue();
    this.id = developer.id = this.id || uuidv4();
    const observable = this.isNew 
      ? this.developerService.create(developer as DeveloperCreateDto)
      : this.developerService.update(developer as DeveloperUpdateDto);
    observable.subscribe(result => {
      this.saving = false;
      if (!result.success) {
        this.snackBar.open('Falha ao salvar desenvolvedor', 'OK', { duration: 3000 });
        return;
      }

      this.snackBar.open('Desenvolvedor salvo com sucesso', 'OK', { duration: 3000 });
      this.close();
    }, (error: HttpErrorResponse) => {
      this.saving = false;
      const result = error.error as Result;
      if (error.status === Status.Conflict) {
        const existsLogin = 'Developer with Login already exist';
        if (result.errorMessages.some(m => m.search(existsLogin) !== -1)) {
          this.snackBar.open('Já existe um desenvolvedor com este login cadastrado!', 'OK', { duration: 3000 });
          return;
        }
      }

      if (error.status === Status.Invalid) {
        const invalidCpf = 'Parameter CPF is not valid';
        if (result.errorMessages.some(m => m.search(invalidCpf) !== -1)) {
          this.snackBar.open('CPF informado é inválido!', 'OK', { duration: 3000 });
          return;
        }
        this.snackBar.open('Existem campos inválidos!', 'OK', { duration: 3000 });
        return;
      }

      this.snackBar.open('Falha ao salvar desenvolvedor', 'OK', { duration: 3000 });
    });
  }

  valid(): boolean {
    if (this.form.invalid) {
      this.snackBar.open('Existem campos inválidos!', 'OK', { duration: 3000 });
      return false;
    }

    return true;
  }

  close() {
    this.router.navigate(['developers']);
  }
}
