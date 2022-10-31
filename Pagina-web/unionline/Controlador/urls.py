from django.urls import path
from django.contrib.auth import views as auth_views
from .views import inicio, iniciar_sesion, crearCuenta, perfil
from .views import inicio, login, crearCuenta, perfil, paginaPrinc


urlpatterns = [
    #Links para redireccionar a otras paginas
    path('', inicio, name='inicio'),
    path('iniciar_sesion', iniciar_sesion, name='iniciar_sesion'),
    path('registrarse', crearCuenta, name ='registrarse'),
    path('perfil', perfil, name ='perfil'),
    path('home', paginaPrinc, name ='home'),

]
