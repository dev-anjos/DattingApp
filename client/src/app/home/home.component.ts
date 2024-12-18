import { Component, inject} from '@angular/core';
import { RegisterComponent } from "../register/register.component";

import { RegisterService } from '../_services/register.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent  {
  registerService = inject(RegisterService);
  registerMode = false;


  //ngOnInit() serve para executar algum codigo que precisa ser executado
  //ao iniciar o componente ou seja, precisamos do Usuarios para serem exibidos

  /**
   * Inicializa o componente, carregando os usuarios e setando o valor do
   * registerMode com base no valor retornado pelo RegisterService
   */
  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  // getUSers(){
  //   this.http.get('https://localhost:5001/api/users').subscribe({
  //     next: response => this.users = response,
  //     error: error => console.log(error),
  //     complete: () => console.log(this.users)
  //   })
  // }

}
