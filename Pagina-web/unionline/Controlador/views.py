from tkinter import EXCEPTION
from webbrowser import get
from django.shortcuts import render, redirect, get_object_or_404
from django.contrib import auth, messages
from . models import Cbr, ClasProp, DuennoProp, Usuario, TUsuario,Comuna,Region,Provincia,Solicitud,Tramite, Direccion, TTramite, Propiedad, CarCompra
from django.contrib.auth.models import User
from django.contrib.auth.forms import UserCreationForm
from werkzeug.security import generate_password_hash, check_password_hash
import mysql.connector as mysql
from django.contrib.auth import authenticate, login
from rut_chile import rut_chile
from django.contrib.auth.decorators import login_required
from datetime import datetime
from django.http import JsonResponse
from django.utils.decorators import method_decorator
from django.views.decorators.csrf import csrf_exempt
from django.views.generic import TemplateView
from django.db.models import Q
from .forms import FormFormularioForm

def listar_carrito(rut):
    carrito = CarCompra.objects.raw("SELECT id_carrito, id_soli, id_tramite, nombre_tramite, valor_tramite FROM UNIONLINE.CAR_COMPRA inner join UNIONLINE.SOLICITUD on SOLICITUD_id_soli = id_soli inner join UNIONLINE.TRAMITE on TRAMITE_id_tramite = id_tramite inner join UNIONLINE.USUARIO on USUARIO_id_usuario = id_usuario where CAR_COMPRA.estado = 0 and rut_usuario = %s;",[rut])
    valor = 0
    return carrito
def listar_tramites():
    tramites = TTramite.objects.all()
    return tramites

def inicio(request):
    #agregar a todas las ventanas de cliente
    tramites = listar_tramites()
    usu = request.user
    carrito = listar_carrito(usu.username)
    valor=0
    can_carrito = 0
    for i in carrito:
        valor += int(i.valor_tramite)
        can_carrito += 1
    miles_translator = str.maketrans(".,", ",.")
    valor = "{:,}".format(valor).translate(miles_translator)
    
    ######
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
        'provincia': provincia,
        'can_carrito': can_carrito,
        'carrito':carrito,
        'tramites':tramites,
        'valor':valor,
        
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

@login_required(login_url='/iniciar_sesion')
def perfil(request):
    #agregar a todas las ventanas de cliente
    tramites = listar_tramites()
    usu = request.user
    carrito = listar_carrito(usu.username)
    valor=0
    can_carrito = 0
    for i in carrito:
        valor += int(i.valor_tramite)
        can_carrito += 1
    miles_translator = str.maketrans(".,", ",.")
    valor = "{:,}".format(valor).translate(miles_translator)
    
    ######
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
        'tramites': tramites,
        'carrito':carrito,
        'valor':valor,
        'can_carrito': can_carrito,
        }
    return render(request, 'templates/perfil-cliente.html', data)



def paginaPrinc(request,id):
    #agregar a todas las ventanas de cliente
    tramites = listar_tramites()
    usu = request.user
    carrito = listar_carrito(usu.username)
    valor=0
    can_carrito = 0
    for i in carrito:
        valor += int(i.valor_tramite)
        can_carrito += 1
    miles_translator = str.maketrans(".,", ",.")
    valor = "{:,}".format(valor).translate(miles_translator)
    ######
    cbr = Cbr.objects.raw('SELECT * FROM UNIONLINE.CBR join UNIONLINE.HOR_ATENCION on UNIONLINE.CBR.HOR_ATENCION_id_horario = UNIONLINE.HOR_ATENCION.id_horario where id_cbr = %s;',[id])
    #cbr = Cbr.objects.raw('SELECT * FROM UNIONLINE.CBR join UNIONLINE.HOR_ATENCION on UNIONLINE.CBR.HOR_ATENCION_id_horario = UNIONLINE.HOR_ATENCION.id_horario where id_cbr = 1;')
    data ={
        'tramites':tramites,
        'Cbr':cbr,
        'carrito':carrito,
        'valor':valor,
        'can_carrito': can_carrito,
        
    }
    return render(request, 'templates/pagina-principal.html',data)


def consultasCom(request):
     #agregar a todas las ventanas de cliente
    tramites = listar_tramites()
    usu = request.user
    carrito = listar_carrito(usu.username)
    valor=0
    can_carrito = 0
    for i in carrito:
        valor += int(i.valor_tramite)
        can_carrito += 1
    miles_translator = str.maketrans(".,", ",.")
    valor = "{:,}".format(valor).translate(miles_translator)
    
    ######
    queryset=request.POST.get("rut")
    clas= ''
    if queryset:
        clas= ClasProp.objects.filter(
            Q(rut_empresa__icontains = queryset) |
            Q(razon_social__icontains = queryset)|
            Q(foja__icontains = queryset)|
            Q(numero__icontains = queryset)|
            Q(anno__icontains = queryset)
        ).distinct()
    data ={
        'tramites':tramites,
        'clas': clas,
        'carrito':carrito,
        'valor':valor,
        'can_carrito': can_carrito,
        
    }
    return render(request, 'templates/consultasCom.html' , data)


def consultasProp(request):
     #agregar a todas las ventanas de cliente
    tramites = listar_tramites()
    usu = request.user
    carrito = listar_carrito(usu.username)
    valor=0
    can_carrito = 0
    for i in carrito:
        valor += int(i.valor_tramite)
        can_carrito += 1
    miles_translator = str.maketrans(".,", ",.")
    valor = "{:,}".format(valor).translate(miles_translator)
    
    ######
    duenno=''
    if request.method == 'POST':
        if request.POST.get("rut") != '':
            queryset= request.POST.get("rut")
            duenno= DuennoProp.objects.raw('SELECT * FROM UNIONLINE.PROPIEDAD join UNIONLINE.DIRECCION on UNIONLINE.PROPIEDAD.DIRECCION_id_direccion = UNIONLINE.DIRECCION.id_direccion join UNIONLINE.CLAS_PROP on UNIONLINE.PROPIEDAD.CLAS_PROP_id_clas = UNIONLINE.CLAS_PROP.id_clas join UNIONLINE.DUENNO_PROP on UNIONLINE.PROPIEDAD.DUENNO_PROP_id_duenno = UNIONLINE.DUENNO_PROP.id_duenno join UNIONLINE.TIPO_PROPIEDAD on UNIONLINE.PROPIEDAD.TIPO_PROPIEDAD_id_tipoP = UNIONLINE.TIPO_PROPIEDAD.id_tipoP join UNIONLINE.COMUNA on UNIONLINE.DIRECCION.COMUNA_id_comuna = UNIONLINE.COMUNA.id_comuna join UNIONLINE.PROVINCIA on UNIONLINE.COMUNA.PROVINCIA_id_provincia= UNIONLINE.PROVINCIA.id_provincia join UNIONLINE.REGION on UNIONLINE.PROVINCIA.REGION_id_region = UNIONLINE.REGION.id_region ="%s";' ,[queryset] )
        else:
            messages.warning(request, "El campo no puede quedar vacío.")
    data ={
        'tramites':tramites,
        'duenno':duenno,
        'carrito':carrito,
        'valor':valor,
        'can_carrito': can_carrito,
        
    }
    return render(request, 'templates/consultasProp.html',data)

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

def listar_tra(request, id):
    #agregar a todas las ventanas de cliente
    tramites = listar_tramites()
    usu = request.user
    carrito = listar_carrito(usu.username)
    valor=0
    can_carrito = 0
    for i in carrito:
        valor += int(i.valor_tramite)
        can_carrito += 1
    miles_translator = str.maketrans(".,", ",.")
    valor = "{:,}".format(valor).translate(miles_translator)
    ######
    tramite = Tramite.objects.raw("SELECT * FROM UNIONLINE.TRAMITE where estado = 'Vigente' and T_TRAMITE_id_tipoT = %s;", [id])
    usu = request.user
    carrito = listar_carrito(usu.username)
    valor=0
    for i in carrito:
        valor += int(i.valor_tramite)
    miles_translator = str.maketrans(".,", ",.")
    valor = "{:,}".format(valor).translate(miles_translator)
    data={
        'tramite': tramite,
        'tramites':tramites,
        'valor': valor,
        'id_tramite':id,
        'carrito':carrito,
        'can_carrito': can_carrito,
        }
    return render(request, 'templates/listar_tramite.html', data)

@login_required(login_url='/iniciar_sesion')
def solicitar_tra(request, id):
    #agregar a todas las ventanas de cliente
    tramites = listar_tramites()
    usu = request.user
    carrito = listar_carrito(usu.username)
    valor=0
    can_carrito = 0
    for i in carrito:
        valor += int(i.valor_tramite)
        can_carrito += 1
    miles_translator = str.maketrans(".,", ",.")
    valor = "{:,}".format(valor).translate(miles_translator)
    ######
    tramite = Tramite.objects.raw('SELECT * FROM UNIONLINE.TRAMITE WHERE id_tramite = %s',[id])
    foja_prop = 828
    propiedad = Propiedad.objects.raw("SELECT id_propiedad, descripcion, id_clas, foja, numero, YEAR(anno) as fecha FROM UNIONLINE.PROPIEDAD inner join UNIONLINE.CLAS_PROP on UNIONLINE.PROPIEDAD.CLAS_PROP_id_clas = UNIONLINE.CLAS_PROP.id_clas where foja = %s;",[foja_prop])
    if request.method == 'POST':
        if 'solicitar' in request.POST:    
            try:       
                solicitud = Solicitud()
                now = datetime.now()
                solicitud.fecha_solicitud = now.strftime("%Y-%m-%d %H:%M:%S")
                solicitud.estado = "En Proceso"
                solicitud.numero_seguimiento = "SO-21"
                solicitud.comentario = ""
                usu = request.user
                usuario = Usuario.objects.raw("SELECT * FROM UNIONLINE.USUARIO where T_USUARIO_id_tipoU = 5 and rut_usuario = %s;",[usu.username]) 
                for i in usuario:
                    id_usuario = i.id_usuario
                    break
                solicitud.usuario_id_usuario = get_object_or_404(Usuario,pk= id_usuario )
                clas_prop = get_object_or_404(ClasProp,foja = foja_prop)
                id_prop = get_object_or_404(Propiedad,clas_prop_id_clas=clas_prop )
                solicitud.propiedad_id_propiedad = id_prop
                id_tramite = get_object_or_404(Tramite, id_tramite = id)
                solicitud.tramite_id_tramite = id_tramite
                solicitud.save()
                editar_solicitud = get_object_or_404(Solicitud,numero_seguimiento = "SO-21")
                id_solicitud = "SO-000" + str(editar_solicitud.pk)
                editar_solicitud.numero_seguimiento = id_solicitud
                editar_solicitud.save()
                carrito = CarCompra()
                carrito.solicitud_id_soli = editar_solicitud
                carrito.estado = 0
                carrito.save()
                messages.success(request, "Solicitud generada correctamente")
            except Exception as e:
                messages.error(request, str(e) )

    
    data={
        'tramite': tramite,
        'prop' : propiedad,
        'carrito':carrito,
        'tramites':tramites,
        'valor':valor,
        'can_carrito': can_carrito,
        }

    return render(request, 'templates/solicitar_tramite.html', data)

def eliminar_carrito(request, id_solicitud, id_car):
    carrito = get_object_or_404(CarCompra, pk = id_car)
    carrito.delete()
    solicitud = get_object_or_404(Solicitud, pk = id_solicitud)
    solicitud.delete()
    return redirect(to="perfil")