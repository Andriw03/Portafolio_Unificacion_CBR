from django.urls import path
from django.contrib.auth import views as auth_views
from .views import inicio, iniciar_sesion, crearCuenta, perfil, formularioUser, inicioadmin, regDirector
from .views import inicio, crearCuenta, perfil, paginaPrinc, consultasCom, formularioUser, conservador, listar_tra, solicitar_tra, eliminar_carrito, inicioadmin, regDirector,carrito_pagar
from .views import inicio, crearCuenta, perfil, paginaPrinc, consultasProp, formularioUser, conservador, consultasCom, inicioadmin, regDirector
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
    path('inicioadmin', inicioadmin, name='inicioadmin' ),
    path('regDirector', regDirector, name='regDirector' ),
    path(r'^listar/(?P<id>\d+)/$', listar_tra, name='listar'),
    path(r'^solicitar/(?P<id>\d+)/$', solicitar_tra, name='solicitar'),
    path(r'^eliminar_carrito/(?P<id_solicitud>\d+)/(?P<id_car>\d+)/$', eliminar_carrito, name="eliminar_carrito"),
    path('carrito_pagar',carrito_pagar, name='carrito_pagar' )

]
