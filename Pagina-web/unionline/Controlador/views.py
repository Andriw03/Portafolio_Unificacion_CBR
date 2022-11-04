from tkinter import EXCEPTION
from webbrowser import get
from django.shortcuts import render, redirect, get_object_or_404
from django.contrib import auth, messages
from . models import Cbr, ClasProp, DuennoProp, Usuario, TUsuario,Comuna,Region,Provincia,Solicitud,Tramite, Direccion, TTramite
from django.contrib.auth.models import User
from django.contrib.auth.forms import UserCreationForm
from werkzeug.security import generate_password_hash, check_password_hash
import mysql.connector as mysql
from django.contrib.auth import authenticate, login
from rut_chile import rut_chile
from django.contrib.auth.decorators import login_required

from django.http import JsonResponse
from django.utils.decorators import method_decorator
from django.views.decorators.csrf import csrf_exempt
from django.views.generic import TemplateView
from django.db.models import Q
from .forms import FormFormularioForm

def listar_tramites():
    tramites = TTramite.objects.all()
    return tramites

def inicio(request):
    tramites = listar_tramites()
    region = Region.objects.all()
    comuna = ''
    cbr = ''
    provincia = ''
    if request.method == 'POST':
        if 'pRegion' in request.POST:           
            provincia = Provincia.objects.filter(Q(region_id_region_id=request.POST.get('cmbRegion')))
            direcion = Direccion()
            comunaID = Comuna()
            comunaID = Comuna.objects.filter()
            cbr = Cbr.objects.raw('SELECT * FROM UNIONLINE.CBR join UNIONLINE.DIRECCION on UNIONLINE.CBR.DIRECCION_id_direccion = UNIONLINE.DIRECCION.id_direccion join UNIONLINE.COMUNA on UNIONLINE.DIRECCION.COMUNA_id_comuna = UNIONLINE.COMUNA.id_comuna join UNIONLINE.PROVINCIA on UNIONLINE.COMUNA.PROVINCIA_id_provincia = UNIONLINE.PROVINCIA.id_PROVINCIA  where REGION_id_region = %s',[request.POST.get('cmbRegion')])
        elif 'pProvincia' in request.POST:          
            comuna = Comuna.objects.filter(Q(provincia_id_provincia=request.POST.get('cmbProvincia')))
            cbr = Cbr.objects.raw('SELECT * FROM UNIONLINE.CBR join UNIONLINE.DIRECCION on UNIONLINE.CBR.DIRECCION_id_direccion = UNIONLINE.DIRECCION.id_direccion join UNIONLINE.COMUNA on UNIONLINE.DIRECCION.COMUNA_id_comuna = UNIONLINE.COMUNA.id_comuna join UNIONLINE.PROVINCIA on UNIONLINE.COMUNA.PROVINCIA_id_provincia = UNIONLINE.PROVINCIA.id_PROVINCIA  where id_provincia = %s',[request.POST.get('cmbProvincia')])
        elif 'pComuna' in request.POST:           
            ##provincia = Provincia.objects.filter(Q(region_id_region_id=request.POST.get('cmbRegion')))
            cbr = Cbr.objects.raw('SELECT * FROM UNIONLINE.CBR join UNIONLINE.DIRECCION on UNIONLINE.CBR.DIRECCION_id_direccion = UNIONLINE.DIRECCION.id_direccion join UNIONLINE.COMUNA on UNIONLINE.DIRECCION.COMUNA_id_comuna = UNIONLINE.COMUNA.id_comuna  where id_comuna = %s',[request.POST.get('cmbComuna')])
   
    data={
        'tramites':tramites,
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
        usuario.rut_usuario = request.POST.get('rut')
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
        user.username = request.POST.get('rut')
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
                return redirect(to="iniciar_sesion")
            except Exception as e:
                mensaje = "No se ha podido guardar el usuario: " + str(e)
                messages.warning(request, mensaje)
            
        
    return render(request, 'registration/registrar.html')


@login_required
def perfil(request):
    tramites = listar_tramites()
    usuariocbr = Usuario()
    usuariocli = User()
    usuariocli = request.user
    
    cliente = usuariocbr
    tramite = '' 

    cliente = get_object_or_404(Usuario, rut_usuario=usuariocli.username, t_usuario_id_tipou=5)
    tramite = Solicitud.objects.raw('SELECT * FROM UNIONLINE.SOLICITUD join UNIONLINE.TRAMITE on SOLICITUD.TRAMITE_id_tramite = TRAMITE.id_tramite join UNIONLINE.USUARIO on SOLICITUD.USUARIO_id_usuario = USUARIO.id_usuario WHERE USUARIO.rut_usuario = %s',[cliente.rut_usuario])
            
    data={
        'cliente': cliente,
        'tramite': tramite,
        'tramites': tramites
        }
    return render(request, 'templates/perfil-cliente.html', data)



def paginaPrinc(request,id):
    tramites = listar_tramites()
    cbr = Cbr.objects.raw('SELECT * FROM UNIONLINE.CBR join UNIONLINE.HOR_ATENCION on UNIONLINE.CBR.HOR_ATENCION_id_horario = UNIONLINE.HOR_ATENCION.id_horario where id_cbr = %s;',[id])
    #cbr = Cbr.objects.raw('SELECT * FROM UNIONLINE.CBR join UNIONLINE.HOR_ATENCION on UNIONLINE.CBR.HOR_ATENCION_id_horario = UNIONLINE.HOR_ATENCION.id_horario where id_cbr = 1;')
    data ={
        'tramites':tramites,
        'Cbr':cbr
        
    }
    return render(request, 'templates/pagina-principal.html',data)

def consultas(request):
    queryset= request.GET.get("buscar_anno","buscar_foja","buscar_rut")
    clas=ClasProp.objects.all()
    duenno=DuennoProp.objects.all()
    if queryset:
        clas= ClasProp.objects.filter(
            Q(anno__icontains = queryset)|
            Q(foja__icontains = queryset)
    ).distinct
        duenno = DuennoProp.objects.filter(
            Q(rut_duenno__icontains = queryset)

        ).distinct
         
    return render(request, 'templates/consultasProp.html')

def formularioUser(request):
    return render(request, 'templates/formulario.html')

def procesar_formulario(request):
    form = FormFormularioForm()
    if form.is_valid():
        form.save()
        messages.success(request, 'Formulario insertado correctamente.')
        form = FormFormularioForm()
    else:
        messages.error(request, 'Error al insertar formulario. Revise los datos.')

    return render(request, 'templates/formulario.html', {"form":form, "mensaje": 'OK'})


def conservador(request):

    return render(request, 'templates/conservador.html')