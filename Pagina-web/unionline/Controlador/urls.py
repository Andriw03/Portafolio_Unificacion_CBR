from django.urls import path, include, re_path
from django.urls import path, include,re_path
from django.urls import re_path as url
from django.contrib import admin
from django.contrib.auth import views as auth_views
from django.contrib.auth.views import LoginView, PasswordResetView, PasswordResetDoneView, PasswordResetConfirmView, PasswordResetCompleteView
from .views import inicio, iniciar_sesion, crearCuenta, perfil, formularioUser, inicioadmin, regDirector, show_create, transferencia, webpay_plus_commit, webpay_plus_create, listarDirector, Eliminardirector, EditarDirector,ediciondirector, EditarCliente, edicionCliente
from .views import inicio, crearCuenta, perfil, paginaPrinc, consultasCom, formularioUser, conservador, listar_tra, solicitar_tra, eliminar_carrito, inicioadmin, regDirector,carrito_pagar, EditarDirector, ediciondirector, EditarCliente, edicionCliente
from .views import inicio, crearCuenta, perfil, paginaPrinc, consultasProp, formularioUser, conservador, consultasCom, inicioadmin, regDirector,agregar_cbr, listar_cbr, modificar_cbr, eliminar_cbr, EditarDirector, ediciondirector, EditarCliente, edicionCliente
from django.conf.urls.static import static
from django.conf import settings

urlpatterns = [
    #Links para redireccionar a otras paginas
    path('', inicio, name='inicio'),
    path('iniciar_sesion', iniciar_sesion, name='iniciar_sesion'),
    path('registrarse', crearCuenta, name ='registrarse'),
    path('perfil', perfil, name ='perfil'),
    path('editarCliente/', EditarCliente, name='editarCliente' ),
    path('edicionCliente/', edicionCliente, name='edicionCliente'),
    path(r'^(?P<id>\d+)/$', paginaPrinc, name ='home'),
    #path('home', paginaPrinc, name ='home'),
    path('consultorP', consultasProp, name ='consultorP'),
    path('formulario', formularioUser, name ='formulario'),
    path('conservador', conservador, name='conservador' ),
    path('consultorC', consultasCom, name='consultorC' ),
    path('inicioadmin', inicioadmin, name='inicioadmin' ),
    path('regDirector', regDirector, name='regDirector' ),
    path('listarDirector', listarDirector, name='listarDirector' ),
    path('Eliminardirector/<int:id>/', Eliminardirector, name='Eliminardirector' ),
    path('EditarDirector/<int:id>/', EditarDirector, name='EditarDirector' ),
    path('ediciondirector/', ediciondirector, name='ediciondirector'),
    path(r'^listar/(?P<id>\d+)/$', listar_tra, name='listar'),
    path(r'^solicitar/(?P<id>\d+)/$', solicitar_tra, name='solicitar'),
    path(r'^eliminar_carrito/(?P<id_solicitud>\d+)/(?P<id_car>\d+)/$', eliminar_carrito, name="eliminar_carrito"),
    path('carrito_pagar',carrito_pagar, name='carrito_pagar'),
    url(r'^create/$', webpay_plus_create, name="create"),
    url(r'^commit/$', webpay_plus_commit, name="commit"),
    url(r'^create_2/$', show_create, name="create_2"),
    url(r'^transferencia/$', transferencia, name="transferencia"),
    path('agregarCbr', agregar_cbr, name='agregarCbr' ),
    path('listarCbr', listar_cbr, name='listarCbr' ),
    path('modificarCbr/<id>/', modificar_cbr, name='modificarCbr' ),
    path('eliminarCbr/<id>/', eliminar_cbr, name='eliminarCbr' ),
    #url(r'^reset/password_reset', PasswordResetView.as_view, {'template_name': 'registration/password_reset_form.html','email_tamplate_name': 'registration/password_reset_email.html'}, name='password_reset'),
    #url(r'^reset/password_reset_done', password_reset_done, {'template_name': 'registration/password_reset_done.html'}, name='password_reset_done' ),
    #url(r'^reset/(?P<uidb64>[0-9A-Za-z_\-]+)/(?P<token>.+)/$', password_reset_confirm, {'template_name': 'registration/password_reset_confirm.html'}, name='password_reset_confirm'),
    #url(r'^reset/done', password_reset_complete, {'template_name': 'registration/password_reset_complete.html'}, name='password_reset_complete'),
    path('reset/password_reset', PasswordResetView.as_view(template_name='registration/password_reset_forms.html', email_template_name="registration/password_reset_email.html"), name = 'password_reset'),
    path('reset/password_reset_done', PasswordResetDoneView.as_view(template_name='registration/password_reset_done.html'), name = 'password_reset_done'),
    re_path(r'^reset/(?P<uidb64>[0-9A-za-z_\-]+)/(?P<token>.+)/$', PasswordResetConfirmView.as_view(template_name='registration/password_reset_confirm.html'), name = 'password_reset_confirm'),
    path('reset/done',PasswordResetCompleteView.as_view(template_name='registration/password_reset_complete.html') , name = 'password_reset_complete'),

]
