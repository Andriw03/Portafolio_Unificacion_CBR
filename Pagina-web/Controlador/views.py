from django.shortcuts import render
from django.contrib.auth import login,authenticate
from django.contrib import auth
from werkzeug.security import generate_password_hash, check_password_hash


# Create your views here.


def pagina_inicio(request):
    return render (request, 'templates/inicio.html')


def inicio_sesion(request):

    return render (request, 'registration/login.html' )

def perfil_cliente(request):

    return render (request, 'templates/perfil-cliente.html' )