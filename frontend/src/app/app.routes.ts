import { Routes } from '@angular/router';
import { PessoaList } from './pages/pessoa-list/pessoa-list';
import { PessoaCartao } from './pages/pessoa-cartao/pessoa-cartao';


export const routes: Routes = [
    {path: '', component: PessoaList},
    {path: 'pessoa/:id', component: PessoaCartao}
];
