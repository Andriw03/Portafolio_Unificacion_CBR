from tkinter import EXCEPTION
from webbrowser import get
from django.shortcuts import render, redirect
from django.contrib import auth, messages
from . models import Cbr, ClasProp, DuennoProp, Usuario, TUsuario,Comuna,Region,Provincia
from django.contrib.auth.models import User
from django.contrib.auth.forms import UserCreationForm
from werkzeug.security import generate_password_hash, check_password_hash
import mysql.connector as mysql
from django.contrib.auth import authenticate, login
from rut_chile import rut_chile


def cifrar(contrasenna):
    encry = generate_password_hash(contrasenna, 'sha256')
    return encry

def desifrar (encry):
    
    check_password_hash(encry,Usuario.contrasenna)
    return check_password_hash

def inicio(request):

    comuna = Comuna.objects.all()
    region = Region.objects.all()
    cbr = Cbr.objects.all()
    provincia = Provincia.objects.all()
    data={
        'comuna': comuna,
        'region': region,
        'cbr' : cbr,
        'provincia': provincia
    }
    return render(request, 'templates/inicio.html',data)


def iniciar_sesion(request):
    
    if request.method == 'POST':
        rut = request.POST.get('rut')
        contraseña = request.POST.get('password')
        userLogin = authenticate(request,username=rut, password=contraseña)
        #valida que tipo de usuario se está iniciando sesión
        try:
            if userLogin is not None:
                login(request, userLogin)
                userA = request.user
                
                return redirect(to="perfil")
            else:
                mensaje = 'Error, usuario no encontrado'
                messages.error(request, mensaje)
                """
                try:
                    if Usuario.objects.get(userD=userA):
                        return redirect(to="kine")
                   
                except:
                    if Usuario.objects.get(userD=userA):
                        return redirect(to="Paciente")
                """
        except ValueError:
            messages.error(request, ValueError )
           
                
    return render(request, 'registration\iniciar_sesion.html')

def crearCuenta(request):

    if request.method == 'POST':
        usuario = Usuario()
        cbr = Cbr()
        tipoU = TUsuario()
        usuario.rut_usuario = request.POST.get('RUT')
        usuario.primer_nombre = request.POST.get('Nombre')
        usuario.segundo_nombre = request.POST.get('Segundo_Nombre')
        usuario.primer_apellido = request.POST.get('Primer_Apellido')
        usuario.segundo_apellido = request.POST.get('Segundo_Apellido')
        usuario.telefono = request.POST.get('Telefono')
        usuario.correo_electronico = request.POST.get('Correo')
        usuario.contrasenna = request.POST.get('Contraseña')

        cbr.id_cbr = 1

        tipoU.id_tipou = 5

        usuario.cbr_id_cbr = cbr
        usuario.t_usuario_id_tipou = tipoU

        user = User()
        user.email = usuario.correo_electronico
        user.set_password(request.POST.get('Contraseña'))
        user.username = usuario.rut_usuario
        user.first_name = request.POST.get('Nombre')
        user.last_name = request.POST.get('Primer_Apellido')
        

        if usuario.rut_usuario == "" or usuario.primer_nombre == "" or usuario.segundo_nombre == "" or usuario.primer_apellido == "" or usuario.segundo_apellido == "" or usuario.telefono == "" or usuario.correo_electronico == "" or usuario.contrasenna == "":
            messages.warning(request, 'Los campos no pueden quedar vacios.')
            return redirect('registrarse')
        else:
            try:
                user.save()
                usuario.contrasenna = user.password
                usuario.save()
                messages.success(request, "Cuenta Creada Correctamente")
                return redirect(to="login")
            except:
                messages.success(request, "No se ha podido guardar el usuario")
            
        
    return render(request, 'registration/registrar.html')



def perfil(request):
   
   

    return render(request, 'templates/perfil-cliente.html')


def paginaPrinc(request):



    return render(request, 'templates/pagina-principal.html')

def consultas(request):
    duenno = DuennoProp.objects.all()
    prop= ClasProp.objects.all()

    data={
        'duenno' : duenno,
        'prop' : prop

    }


    return render(request, 'templates/consultasProp.html', data)

 

