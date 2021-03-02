import { BrowserModule } from '@angular/platform-browser';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { UsuariosEditarComponent } from './usuarios-editar/usuarios-editar.component'
import { UsuarioService } from './servicios/usuario.service';
import { AsignaturasComponent } from './asignaturas/asignaturas.component';
import { AsignaturasEditarComponent } from './asignaturas-editar/asignaturas-editar.component';
import { AsignaturaService } from './servicios/asignatura.service';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatDialogModule } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSnackBar, MatSnackBarModule, SimpleSnackBar } from '@angular/material/snack-bar';
import { ModalDialogComponent } from './modal-dialog/modal-dialog.component';
import { MatSelectModule } from '@angular/material';
import { ConsultasComponent } from './consultas/consultas.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    UsuariosComponent,
    UsuariosEditarComponent,
    AsignaturasComponent,
    AsignaturasEditarComponent,
    ModalDialogComponent,
    ConsultasComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    MatDialogModule, 
    MatInputModule, 
    MatButtonModule, 
    MatCardModule, 
    MatFormFieldModule,
    MatSelectModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'usuarios', component: UsuariosComponent, data:{ animation:{value:'usuario'}}},
      { path: 'editarusuario/:id', component: UsuariosEditarComponent, data: { animation: { value: 'usuarioeditar' } } },
      { path: 'asignaturas', component: AsignaturasComponent,data:{animation:{value:'asignatura'}}},
      { path: 'editarasignatura/:id', component: AsignaturasEditarComponent, data: { animation: { value: 'asignaturaeditar' } } },
      { path: 'consultas', component: ConsultasComponent, data: { animation: { value: 'consultas' } }}
    ]),
    MatSnackBarModule
  ],
  providers: [UsuarioService,AsignaturaService],
  bootstrap: [AppComponent],
  schemas:[CUSTOM_ELEMENTS_SCHEMA],
  entryComponents:[ModalDialogComponent]
})
export class AppModule { }
