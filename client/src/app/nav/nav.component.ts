import { Component, inject } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RegisterService } from '../_services/register.service';
import { TitleCasePipe } from '@angular/common';



@Component({
  selector: 'app-nav',
  standalone: true,
  imports: [FormsModule,BsDropdownModule, RouterLink, RouterLinkActive, TitleCasePipe],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountService = inject(AccountService);
  registerService = inject(RegisterService);

  private router = inject(Router);
  private toaster = inject(ToastrService);
  model: any = {}; // valores de login sao pegados do formulario atraves do ngModel e passados para o login 
  userName: string = '';


  registerToggle() {
    this.registerService.toggleRegisterMode();
  }
  
  login() {
    this.accountService.login(this.model).subscribe({
      
      next: () => {
        this.router.navigateByUrl('/members');
      },
      error: error => {
        this.toaster.error(error.error);
      }
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
