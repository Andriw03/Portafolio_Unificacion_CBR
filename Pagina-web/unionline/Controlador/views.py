from django.shortcuts import render, redirect
from django.contrib import auth
from . models import Cbr, Usuario, TUsuario
from django.contrib.auth.forms import UserCreationForm
from werkzeug.security import generate_password_hash, check_password_hash


def cifrar(contrasenna):
    encry = generate_password_hash(contrasenna, 'sha256')
    return encry

def desifrar (encry):
    
    check_password_hash(encry,Usuario.contrasenna)

    return check_password_hash


def inicio(request):
    return render(request, 'templates/inicio.html')



def login(request):
    return render(request, 'Registration/login.html')

def crearCuenta(request):
    if request.method == 'POST':
        usuario = Usuario()
        cbr = Cbr()
        tipoU = TUsuario()
        usuario.rut_usuario = request.POST.get('RUT')
        usuario.primer_nombre = request.POST.get('Nombre')
        usuario.segundo_nombre = request.POST.get('Segundo_Nombre')
        usuario.segundo_nombre = request.POST.get('Primer_Apellido')
        usuario.segundo_nombre = request.POST.get('Segundo_Apellido')
        usuario.segundo_nombre = request.POST.get('Telefono')
        usuario.segundo_nombre = request.POST.get('Correo')
        usuario.segundo_nombre = request.POST.get('Contraseña')
        tipoU.id_tipoU = 2
        cbr.cbr_id_cbr = 1

        usuario.save()
    return render(request, 'registration/registrar.html')