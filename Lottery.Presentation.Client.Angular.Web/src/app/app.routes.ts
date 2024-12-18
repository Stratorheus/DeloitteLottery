import { Routes } from '@angular/router';
import {HomeComponent} from "./pages/home/home.component";
import {HistoryComponent} from "./pages/history/history.component";
import {NotfoundComponent} from "./pages/notfound/notfound.component";
import {ErrorComponent} from "./components/error/error.component";

//Ideally, the routes should be stored somewhere else as constant strings and referenced here, same in other parts of the app
export const routes: Routes = [
  { path: '', component: HomeComponent }, // Homepage
  { path: 'error', component: ErrorComponent},
  { path: 'history', component: HistoryComponent }, // Draw history
  { path: '**', component: NotfoundComponent }, // Custom 404 page
];
