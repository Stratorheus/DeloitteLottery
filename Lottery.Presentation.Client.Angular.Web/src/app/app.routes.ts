import { Routes } from '@angular/router';
import {HomeComponent} from "./pages/home/home.component";
import {HistoryComponent} from "./pages/history/history.component";
import {NotfoundComponent} from "./pages/notfound/notfound.component";

export const routes: Routes = [
  { path: '', component: HomeComponent }, // Homepage
  { path: 'history', component: HistoryComponent }, // Draw history
  { path: '**', component: NotfoundComponent }, // Custom 404 page
];
