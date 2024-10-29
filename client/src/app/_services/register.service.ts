import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  private registerMode = new BehaviorSubject(false);;
  
  /**
   * Alterna entre o modo de registro ativo ou desativo.
   * Envia um novo valor para o observable registerMode.
   */
  toggleRegisterMode() {
    this.registerMode.next(!this.registerMode.value);
  }

  getRegisterMode() {
    return this.registerMode.asObservable();
  }

  setRegisterMode(event: boolean) {
    this.registerMode.next(event);
  }

}