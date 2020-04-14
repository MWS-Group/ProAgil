import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from './../services/evento.service';
import { Evento } from '../models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventosFiltrados: Evento[];
  eventos: Evento[];
  imagemLargura = 50;
  imagemMargem = 2;
  imagem = false;
  modalRef: BsModalRef;

  filtro: string;

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService
    ) { }

  get filtroLista() {
    return this.filtro;
  }

  set filtroLista(value: string) {
    this.filtro = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  ngOnInit() {
    this.getEventos();
  }

  getEventos() {
    this.eventoService.getAllEvento().subscribe(
      (eventos: Evento[]) => {
        this.eventos = eventos;
        this.eventosFiltrados = this.eventos;
      }, error => {
        console.log(error);
      });
  }

  mostrarImagem() {
    this.imagem = !this.imagem;
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    );
  }

}
