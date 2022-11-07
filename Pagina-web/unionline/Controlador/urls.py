from django.urls import path
from django.contrib.auth import views as auth_views
from .views import inicio, iniciar_sesion, crearCuenta, perfil, formularioUser
from .views import inicio, crearCuenta, perfil, paginaPrinc, consultasCom, formularioUser, conservador, listar_tra, solicitar_tra, eliminar_carrito
from .views import inicio, crearCuenta, perfil, paginaPrinc, consultasProp, formularioUser, conservador, consultasCom
from django.conf.urls.static import static
from django.conf import settings

urlpatterns = [
    #Links para redireccionar a otras paginas
    path('', inicio, name='inicio'),
    path('iniciar_sesion', iniciar_sesion, name='iniciar_sesion'),
    path('registrarse', crearCuenta, name ='registrarse'),
    path('perfil', perfil, name ='perfil'),
    path(r'^(?P<id>\d+)/$', paginaPrinc, name ='home'),
    #path('home', paginaPrinc, name ='home'),
    path('consultorP', consultasProp, name ='consultorP'),
    path('formulario', formularioUser, name ='formulario'),
    path('conservador', conservador, name='conservador' ),
    path('consultorC', consultasCom, name='consultorC' ),
    path(r'^listar/(?P<id>\d+)/$', listar_tra, name='listar'),
    path(r'^solicitar/(?P<id>\d+)/$', solicitar_tra, name='solicitar'),
    path(r'^eliminar_carrito/(?P<id_solicitud>\d+)/(?P<id_car>\d+)/$', eliminar_carrito, name="eliminar_carrito")

]
