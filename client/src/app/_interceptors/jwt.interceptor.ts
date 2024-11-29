import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AccountService } from '../_services/account.service';

export const jwtInterceptor: HttpInterceptorFn = (req, next) => {
  const accountService = inject(AccountService);

  if (accountService.currentUser()) {
    req = req.clone ({ // clone para nao alterar o original
      setHeaders: { //cabeçalho, serve para envia informações adicionais
        Authorization: `Bearer ${accountService.currentUser()?.token}` 
      }
    })
  }
  return next(req);
};
