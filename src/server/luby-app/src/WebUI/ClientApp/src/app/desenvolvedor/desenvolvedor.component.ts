import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NotificationService } from '../notification.service';
import { PaginatedListOfDesenvolvedorDto, DesenvolvedorClient, DesenvolvedorDto, CreateDesenvolvedorCommand, UpdateDesenvolvedorCommand, ProjetoDto, ProjetoClient } from '../web-api-client';

@Component({
  selector: 'app-desenvolvedor',
  templateUrl: './desenvolvedor.component.html',
  styleUrls: ['./desenvolvedor.component.css']
})
export class DesenvolvedorComponent {
  desenvolvedorVM: PaginatedListOfDesenvolvedorDto;
  desenvolvedorSelecionado: DesenvolvedorDto;
  modalNovoDesenvolvedor: BsModalRef;
  modalEdicaoDesenvolvedor: BsModalRef;
  modalExclusaoDesenvolvedor: BsModalRef;
  projetos: ProjetoDto[];
  novoDesenvolvedor: any = {};
  projetoSelecionado: number;
  itensPorPagina: number = 5;

  constructor(private client: DesenvolvedorClient, clientProjeto: ProjetoClient, private modalService: BsModalService, private notification: NotificationService) {
    client.getDesenvolvedorWithPagination(1, this.itensPorPagina).subscribe(result => {
      this.desenvolvedorVM = result;
    }, error => console.error(error));

    clientProjeto.getAll().subscribe(result => {
      this.projetos = result;
    }, error => console.error(error));
  } 

  onPageChange(pageNumber) {
    this.client.getDesenvolvedorWithPagination(pageNumber, this.itensPorPagina).subscribe(result => {
      this.desenvolvedorVM = result;
    }, error => console.error(error));
  }

  showModalCreate(template: TemplateRef<any>): void {
    this.modalNovoDesenvolvedor = this.modalService.show(template);
  }

  showModalUpdate(template: TemplateRef<any>, d: DesenvolvedorDto) {
    this.desenvolvedorSelecionado = new DesenvolvedorDto(d);
    this.modalEdicaoDesenvolvedor = this.modalService.show(template);
  }

  saveDesenvolvedor(): void {
    let dev = DesenvolvedorDto.fromJS({
      id: 0,
      nome: this.novoDesenvolvedor.nome,
      cpf: this.novoDesenvolvedor.cpf,
      senha: this.novoDesenvolvedor.senha,
      email: this.novoDesenvolvedor.email,
      projetoId: this.projetoSelecionado
    });

    this.client.create(<CreateDesenvolvedorCommand>{
      nome: this.novoDesenvolvedor.nome,
      senha: this.novoDesenvolvedor.senha,
      email: this.novoDesenvolvedor.email,
      cpf: this.novoDesenvolvedor.cpf,
      projetoId: this.projetoSelecionado
    }).subscribe(
      result => {
        dev.id = result;
        this.desenvolvedorVM.items.push(dev);
        this.modalNovoDesenvolvedor.hide();
        this.novoDesenvolvedor = {};
        this.notification.showSuccess("Desenvolvedor salvo com sucesso!", '');
      },
      error => {
        let errors = JSON.parse(error.response);

        if (errors) {
          this.novoDesenvolvedor.nomeError = errors.errors.Nome ? errors.errors.Nome[0] : null;
          this.novoDesenvolvedor.cpfError = errors.errors.CPF ? errors.errors.CPF[0] : null;
          this.novoDesenvolvedor.senhaError = errors.errors.Senha ? errors.errors.Senha[0] : null;
          this.novoDesenvolvedor.emailError = errors.errors.Email ? errors.errors.Email[0] : null;
          this.novoDesenvolvedor.projetoError = errors.errors.ProjetoId ? errors.errors.ProjetoId[0] : null;
        }
      }
    );
  }

  updateDesenvolvedor() {
    this.client.update(this.desenvolvedorSelecionado.id, UpdateDesenvolvedorCommand.fromJS(this.desenvolvedorSelecionado))
      .subscribe(
        () => {
          this.modalEdicaoDesenvolvedor.hide();
          this.desenvolvedorVM.items = this.desenvolvedorVM.items.filter(t => t.id != this.desenvolvedorSelecionado.id);
          this.desenvolvedorVM.items.push(this.desenvolvedorSelecionado);
          this.desenvolvedorSelecionado = new DesenvolvedorDto();

          this.notification.showSuccess("Desenvolvedor atualizado com sucesso!", '');
        },
        error => {
          let errors = JSON.parse(error.response);

          if (errors) {
            this.novoDesenvolvedor.nomeError = errors.errors.Nome ? errors.errors.Nome[0] : null;
            this.novoDesenvolvedor.cpfError = errors.errors.CPF ? errors.errors.CPF[0] : null;
            this.novoDesenvolvedor.senhaError = errors.errors.Senha ? errors.errors.Senha[0] : null;
            this.novoDesenvolvedor.emailError = errors.errors.Email ? errors.errors.Email[0] : null;
            this.novoDesenvolvedor.projetoError = errors.errors.ProjetoId ? errors.errors.ProjetoId[0] : null;
          }
        }
      );
  }

  confirmDelete(template: TemplateRef<any>, d: DesenvolvedorDto) {
    this.desenvolvedorSelecionado = d;
    this.modalExclusaoDesenvolvedor = this.modalService.show(template);
  }

  deleteConfirmed(): void {
    this.client.delete(this.desenvolvedorSelecionado.id).subscribe(
      () => {
        this.modalExclusaoDesenvolvedor.hide();
        this.desenvolvedorVM.items = this.desenvolvedorVM.items.filter(t => t.id != this.desenvolvedorSelecionado.id);
        this.notification.showSuccess("Desenvolvedor removido com sucesso!", '');
      },
      error => console.error(error)
    );
  }
}
