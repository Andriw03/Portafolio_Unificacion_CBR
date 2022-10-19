from django.shortcuts import render
from django.contrib import auth
from . models import Usuario
from werkzeug.security import generate_password_hash, check_password_hash


def cifrar(contrasenna):
    encry = generate_password_hash(contrasenna, 'sha256')
    return encry

def desifrar (encry):
    
    check_password_hash(encry,Usuario.contrasenna)

    return check_password_hash




def inicio(request):
    return render(request, 'Controlador/templates/inicio.html')



def login(request):

    return render(request, 'Controlador/registration/login.html')