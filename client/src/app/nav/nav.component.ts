import { Component, inject } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';


@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,BsDropdownModule],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);
  model: any = {}; // valores de login sao pegados do formulario atraves do ngModel e passados para o login 
  userName: string = '';

  
  login() {
    this.accountService.login(this.model).subscribe({
      
      next: response => {
        console.log(response);
      },
      error: error => {
        alert("Senha ou nome de usuario invalido");
        console.warn(error)
        console.log(this.model)
      }
    })
  }

  logout() {
    this.accountService.logout();
  }

}
