import { Component,  inject,  input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountSrvice = inject(AccountService)
  private toaster = inject(ToastrService)
  cancelRegister = output<boolean>() // output é a saida
  model: any = {};

  register() {
    this.accountSrvice.register(this.model).subscribe({
      next: response =>{
        console.log(response);
        this.cancel();
      },
      error: error => {
        console.log(error);
        this.toaster.error(error.error);
      }
    })
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
