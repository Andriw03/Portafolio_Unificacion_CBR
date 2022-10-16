from os import name
from django.conf.urls import include, url
from django.contrib.auth.decorators import login_required
from django.urls import path
from . import views
from . views import pagina_inicio, inicio_sesion, perfil_cliente

urlpatterns =[

path('', pagina_inicio, name = "inicio"),
path('login', inicio_sesion, name = "login"),
path('miperfil', perfil_cliente, name = "miperfil"),

]