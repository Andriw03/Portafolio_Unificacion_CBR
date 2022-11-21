import os
import random
import base64
from django.template.loader import get_template
from xhtml2pdf import pisa
from django.views.generic import View
from io import BytesIO
from django.http import HttpResponse
from django.conf import settings
from django.core.mail import EmailMultiAlternatives
from datetime import datetime
from datetime import datetime as dt
from tkinter import EXCEPTION
from urllib import response
from webbrowser import get
import mysql.connector
from django.contrib import auth, messages
from django.contrib.auth import authenticate, login
from django.contrib.auth.decorators import (login_required,
                                            permission_required,
                                            user_passes_test)
from django.contrib.auth.forms import UserCreationForm
from django.contrib.auth.models import User
from django.db.models import Q
from django.http import JsonResponse
from django.shortcuts import get_object_or_404, redirect, render
from django.utils.decorators import method_decorator
from django.views.decorators.csrf import csrf_exempt
from django.views.generic import TemplateView
from rut_chile import rut_chile
from transbank.error.transbank_error import TransbankError
from transbank.webpay.webpay_plus.transaction import Transaction
from werkzeug.security import check_password_hash, generate_password_hash
from .forms import FormFormularioForm
from .models import (CarCompra, Cbr, ClasProp, Comuna, Direccion, DuennoProp,
                     EstadoPago, HorAtencion, Propiedad, Provincia, Region,
                     Solicitud, TipoPago, Tramite, TTramite, TUsuario, Usuario, Documento,TipoDocumento, Documento)

'''
def llenarCbr():
    replacements = (
        ("á", "a"),
        ("é", "e"),
        ("í", "i"),
        ("ó", "o"),
        ("ú", "u"),
    )
    
    comuna = Comuna.objects.all()
    color=( ['rojo', 'amarillo', 'verde', 'azul', 'marrón', 'negro'])
    for i in comuna:
        cbr = Cbr()
        direccion = Direccion()
        idComuna = get_object_or_404(Comuna, pk = i.id_comuna)
        direccion.nombre_calle = str(random.choice(color)) + " con " + str(random.choice(color))
        direccion.numero_casa = int(random.randrange(10, 2000, 2))
        direccion.comuna_id_comuna = idComuna
        direccion.save()
        cbr.nombre_cbr = "CBR "+ str(i.nombre_comuna)
        comuna_correo = str(i.nombre_comuna).replace(" ","")
        comuna_correo = comuna_correo.lower()
        cbr.correo_cbr = "cbr_" + comuna_correo +"@gmail.com"
        for a,b in replacements:
            cbr.correo_cbr = cbr.correo_cbr.replace(a,b)
        cbr.telefono = str(random.randint(900000000, 999999999))
        atencion = get_object_or_404(HorAtencion, pk=1)
        cbr.hor_atencion_id_horario = atencion
        cbr.direccion_id_direccion = direccion
        
        cbr.save()
    '''

def listar_carrito(rut):
    carrito = CarCompra.objects.raw("SELECT id_carrito, id_soli, id_tramite, nombre_tramite, valor_tramite FROM UNIONLINE.CAR_COMPRA inner join UNIONLINE.SOLICITUD on SOLICITUD_id_soli = id_soli inner join UNIONLINE.TRAMITE on TRAMITE_id_tramite = id_tramite inner join UNIONLINE.USUARIO on USUARIO_id_usuario = id_usuario where CAR_COMPRA.estado = 0 and rut_usuario = %s;",[rut])
    valor = 0
    return carrito
def listar_tramites():
    tramites = TTramite.objects.all()
    #llenarCbr()
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
    if usu.groups.filter(name='Admin').exists():
        return redirect(to="inicioadmin")
    ######
    usu
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
        elif 'pCBR' in request.POST: 
            id_cbr = request.POST.get('cmbCBR')
            print(id_cbr)
            return redirect('home', id=id_cbr)
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
                us = Usuario.objects.raw("SELECT * FROM UNIONLINE.USUARIO where rut_usuario = %s;",[rut])  
                tCliente= get_object_or_404(TUsuario, pk=5)
                tAdmin= get_object_or_404(TUsuario, pk=1)
                for i in us:
                   
                    if i.t_usuario_id_tipou == tCliente:

                        return redirect(to="perfil")
                        
                    elif i.t_usuario_id_tipou == tAdmin:

                        return redirect(to="inicioadmin")
                
                
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
                user.groups.add(2)
                usuario.contrasenna = user.password
                usuario.save()
                messages.success(request, "Cuenta Creada Correctamente")
                return redirect(to="iniciar_sesion")
            except Exception as e:
                mensaje = "No se ha podido guardar el usuario: " + str(e)
                messages.warning(request, mensaje)
            
        
    return render(request, 'registration/registrar.html')

def write_file(data, filename):
    # Convert binary data to proper format and write it on Hard Disk
    with open(filename, 'wb') as file:
        file.write(data)


def readBLOB(emp_id, bioData):
    try:
        connection = mysql.connector.connect(host='unificacion.cmvnu851mzxa.us-east-1.rds.amazonaws.com',
                                        database='UNIONLINE',
                                        user='root',
                                        password='nohomo123')

        cursor = connection.cursor()
        sql_fetch_blob_query = """SELECT * FROM UNIONLINE.DOCUMENTO where id_documento = %s"""

        cursor.execute(sql_fetch_blob_query, (emp_id,))
        record = cursor.fetchall()
        for row in record:
            print("Id = ", row[0], )
            print("Name = ", row[1])
            file = row[2]

            write_file(file, bioData)

    except mysql.connector.Error as error:
        print("Failed to read BLOB data from MySQL table {}".format(error))
        if connection.is_connected():
            cursor.close()
            connection.close()
            print("MySQL connection is closed")

def send_mail(id,correo):
    
    documento= get_object_or_404(Documento , pk=id)
    context = {'correo': correo}
    template = get_template('documento_correo.html')
    content = template.render(context)
    nombreDoc= documento.nombre_doc + '.pdf'

    #readBLOB(id, nombreDoc)

    doc = documento.doc
    #docu = doc.encode(encoding='utf-8')
    # file = open(docu,"wb")
    # for line in open (nombreDoc,"rb").readlines():
    #     file.write(line)
    # file.close()
    
    with open (nombreDoc, "wb") as p:
        #decoderDoc=base64.standard_b64decode(doc)
        p.write(doc) 
        
    
    email = EmailMultiAlternatives(
        'Envío de Trámite',
        'Cbr',
        settings.EMAIL_HOST_USER,
        [correo]

    )
    #email.attach_alternative(content, 'text/html')
    #email.send()
    

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
    tramite = Solicitud.objects.raw('SELECT id_carrito, id_soli,id_tramite, id_usuario, nombre_tramite, numero_seguimiento, SOLICITUD.estado as estado, valor_tramite, correo_electronico FROM UNIONLINE.SOLICITUD inner join UNIONLINE.CAR_COMPRA on UNIONLINE.SOLICITUD.id_soli = UNIONLINE.CAR_COMPRA.SOLICITUD_id_soli join UNIONLINE.TRAMITE on SOLICITUD.TRAMITE_id_tramite = TRAMITE.id_tramite join UNIONLINE.USUARIO on SOLICITUD.USUARIO_id_usuario = USUARIO.id_usuario where CAR_COMPRA.estado = 1 and USUARIO.rut_usuario = %s',[cliente.rut_usuario])
    documento = Documento.objects.raw ('SELECT UNIONLINE.USUARIO.id_usuario, UNIONLINE.DOCUMENTO.doc, UNIONLINE.DOCUMENTO.id_documento, UNIONLINE.DOCUMENTO.nombre_doc FROM UNIONLINE.USUARIO JOIN UNIONLINE.SOLICITUD ON UNIONLINE.USUARIO.id_usuario = UNIONLINE.SOLICITUD.USUARIO_id_usuario JOIN UNIONLINE.DOCUMENTO ON UNIONLINE.SOLICITUD.id_soli = UNIONLINE.DOCUMENTO.SOLICITUD_id_soli where DOCUMENTO.TIPO_DOCUMENTO_id_tipodoc =2')         
    
    data={
        'cliente': cliente,
        'tramite': tramite,
        'tramites': tramites,
        'carrito':carrito,
        'valor':valor,
        'can_carrito': can_carrito,
        'documento' : documento,
        }
    if request.method =='POST':
        id = request.POST.get('mail')
        send_mail(id,usu.email)
        messages.success(request, 'Correo enviado.')
    else: 
        messages.warning(request, 'Imposible enviar el correo.')
    return render(request, 'templates/perfil-cliente.html', data)

@login_required(login_url='/iniciar_sesion')
def EditarCliente (request):
    # cliente=Usuario.objects.get(id_usuario = id)
    
    data = {
        'cliente' : Usuario
    }

    return render(request, "editarCliente.html", data)

@login_required(login_url='/iniciar_sesion')
def edicionCliente (request):
        #usuario.primer_nombre = request.POST.get('Nombre')
    id = int(request.POST['id'])
    primer_nombre = request.POST['Nombre']
        #usuario.segundo_nombre = request.POST.get('Segundo_Nombre')
    sdo_nombre = request.POST['Segundo_Nombre']
        #usuario.primer_apellido = request.POST.get('Primer_Apellido')
    apellido = request.POST['Primer_Apellido']
        #usuario.segundo_apellido = request.POST.get('Segundo_Apellido')
    sdoapellido = request.POST['Segundo_Apellido']
        #usuario.telefono = request.POST.get('Telefono')
    fono = request.POST['Telefono']
        #usuario.correo_electronico = request.POST.get('Correo')
    email = request.POST['Correo']
        #usuario.contrasenna = request.POST.get('Contraseña')
    pwd = request.POST['c']
    #cliente = get_object_or_404(Usuario, id_usuario = id)
    Usuario=Usuario.objects.get(id_usuario = id)
    Usuario.primer_nombre = primer_nombre
    Usuario.segundo_nombre = sdo_nombre
    Usuario.primer_apellido = apellido
    Usuario.segundo_apellido = sdoapellido
    Usuario.telefono = fono
    Usuario.correo_electronico = email
    Usuario.contrasenna = pwd

    Usuario.save()

    return redirect('perfil')


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
    
    clas= ''
    if request.method == 'POST':
        if request.POST.get("rut") != '':
            queryset= request.POST.get("rut")
            clas= ClasProp.objects.raw('SELECT CLAS_PROP.id_clas, CLAS_PROP.foja, CLAS_PROP.numero, CLAS_PROP.razon_social, CLAS_PROP.rut_empresa ,YEAR(CLAS_PROP.anno) as fecha FROM UNIONLINE.CLAS_PROP WHERE rut_empresa = %s;' ,[queryset] )
        else:
            messages.warning(request, "El campo no puede quedar vacío.")
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
            duenno= DuennoProp.objects.raw('SELECT PROPIEDAD.id_propiedad, CLAS_PROP.id_clas, DUENNO_PROP.id_duenno, DUENNO_PROP.rut_duenno, CLAS_PROP.foja, CLAS_PROP.numero, YEAR(CLAS_PROP.anno) as fecha, concat(DUENNO_PROP.primer_nombre," ", DUENNO_PROP.primer_apellido) AS nombre FROM UNIONLINE.PROPIEDAD JOIN DUENNO_PROP ON PROPIEDAD.DUENNO_PROP_id_duenno = DUENNO_PROP.id_duenno JOIN CLAS_PROP ON PROPIEDAD.CLAS_PROP_id_clas = CLAS_PROP.id_clas WHERE rut_duenno = %s;' ,[queryset] )
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

def convertToBinaryData(filename):
    # Convert digital data to binary format
    with open(filename, 'rb') as file:
        binaryData = file.read()
    return binaryData
def insertBLOB(nombre_doc, doc, id_soli):
    print("Inserting BLOB into python_employee table")
    try:
        connection = mysql.connector.connect(host='unificacion.cmvnu851mzxa.us-east-1.rds.amazonaws.com',
                                             database='UNIONLINE',
                                             user='root',
                                             password='nohomo123')

        cursor = connection.cursor()
        sql_insert_blob_query = """ INSERT INTO `UNIONLINE`.`DOCUMENTO`
                          (`nombre_doc`, `doc`, `SOLICITUD_id_soli`, `TIPO_DOCUMENTO_id_tipodoc`) VALUES (%s,%s,%s,1)"""

        documento = convertToBinaryData(doc)
        # Convert data into tuple format
        insert_blob_tuple = (nombre_doc, documento, id_soli)
        result = cursor.execute(sql_insert_blob_query, insert_blob_tuple)
        connection.commit()
        print("Funiono mi rey", result)
        if connection.is_connected():
            cursor.close()
            connection.close()
            print("MySQL connection is closed")

    except mysql.connector.Error as error:
        print("Failed inserting BLOB data into MySQL table {}".format(error))
        if connection.is_connected():
            cursor.close()
            connection.close()
            print("MySQL connection is closed")
    
def findfile(name, path):
    for dirpath, dirname, filename in os.walk(path):
        if name in filename:
            return os.path.join(dirpath, name)

@login_required(login_url='/iniciar_sesion')
def solicitar_tra(request, id):
    #agregar a todas las ventanas de cliente
    tramites = listar_tramites()
    usu = request.user
    list_carrito = listar_carrito(usu.username)
    valor=0
    can_carrito = 0
    for i in list_carrito:
        valor += int(i.valor_tramite)
        can_carrito += 1
    miles_translator = str.maketrans(".,", ",.")
    valor = "{:,}".format(valor).translate(miles_translator)
    ######
    tramite = Tramite.objects.raw('SELECT * FROM UNIONLINE.TRAMITE WHERE id_tramite = %s',[id])
    id_doc = 0
    propiedad = ''
    for i in tramite:
        if i.t_documento == "Copia de cédula de identidad.":
            id_doc = 1
        elif i.t_documento == "Escritura de propiedad.":
            id_doc = 2
        elif i.t_documento == "Copia de cédula de identidad y Escritura de propiedad.":
            id_doc = 3

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
                print("se caera aqui??  " + request.POST.get("foja"))
                foja_prop = request.POST.get("foja")
                clas_prop = get_object_or_404(ClasProp,foja = foja_prop)
                id_prop = get_object_or_404(Propiedad,clas_prop_id_clas=clas_prop )
                solicitud.propiedad_id_propiedad = id_prop
                id_tramite = get_object_or_404(Tramite, id_tramite = id)
                solicitud.tramite_id_tramite = id_tramite
                solicitud.save()
                rut = str(usu.username).replace(".","").replace("-","")
                for i in tramite:
                    if i.t_documento == "Copia de cédula de identidad.":
                        variable = request.POST.get("copiaCedula")
                        variable2 = findfile(variable, "C:/")
                        nombre_doc = "Copia_Carnet_" + rut
                        doc = variable2
                        solicitud_id_soli = solicitud.id_soli
                        insertBLOB(nombre_doc,doc,solicitud_id_soli)
                    elif i.t_documento == "Escritura de propiedad.":
                        variable = request.POST.get("escritura")
                        variable2 = findfile(variable, "C:/")
                        nombre_doc = "Escritura_propiedad_" + rut
                        doc = variable2
                        solicitud_id_soli = solicitud.id_soli
                        insertBLOB(nombre_doc,doc,solicitud_id_soli)
                        
                    elif i.t_documento == "Copia de cédula de identidad y Escritura de propiedad.":
                        variable = request.POST.get("copiaCedula")
                        variable2 = findfile(variable, "C:/")
                        nombre_doc = "Copia_Carnet_" + rut
                        doc = variable2
                        solicitud_id_soli = solicitud.id_soli
                        insertBLOB(nombre_doc,doc,solicitud_id_soli)

                        variable = request.POST.get("escritura")
                        variable2 = findfile(variable, "C:/")
                        nombre_doc = "Escritura_propiedad_" + rut
                        doc = variable2
                        solicitud_id_soli = solicitud.id_soli
                        insertBLOB(nombre_doc,doc,solicitud_id_soli)
                               
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
                messages.error(request, "Error al crear la solicitud: " + str(e) )

        elif 'buscarFoja' in request.POST:
            try:
                foja_prop = request.POST.get("foja")
                propiedad = Propiedad.objects.raw("SELECT id_propiedad, descripcion, id_clas, foja, numero, YEAR(anno) as fecha FROM UNIONLINE.PROPIEDAD inner join UNIONLINE.CLAS_PROP on UNIONLINE.PROPIEDAD.CLAS_PROP_id_clas = UNIONLINE.CLAS_PROP.id_clas where foja = %s;",[foja_prop])
            except Exception as e:
                messages.error(request, str(e))
    
    data={
        'tramite': tramite,
        'prop' : propiedad,
        'carrito': list_carrito,
        'tramites': tramites,
        'valor': valor,
        'can_carrito': can_carrito,
        'id_doc' : id_doc,
        }

    return render(request, 'templates/solicitar_tramite.html', data)

@login_required(login_url='/iniciar_sesion')
def eliminar_carrito(request, id_solicitud, id_car):
    doc = Documento.objects.raw("SELECT * FROM UNIONLINE.DOCUMENTO where SOLICITUD_id_soli = %s;",[id_solicitud])
    for i in doc:
        drop_doc = get_object_or_404(Documento, pk = i.id_documento)
        drop_doc.delete()
    carrito = get_object_or_404(CarCompra, pk = id_car)
    carrito.delete()
    solicitud = get_object_or_404(Solicitud, pk = id_solicitud)
    solicitud.delete()
    return redirect(to="perfil")


@login_required(login_url='/iniciar_sesion')
def inicioadmin(request):
    return render(request, 'templates/inicio_admin.html')

@login_required(login_url='/iniciar_sesion')
def agregar_cbr (request):
    comuna = Comuna.objects.all()
    cbr = ''
    provincia = ''
    aten = HorAtencion.objects.all()
    if request.method == 'POST':
        
        cbr=Cbr()
        dir = Direccion()
        horario= get_object_or_404(HorAtencion, pk= request.POST.get ('cmbHorario'))
        cbr.nombre_cbr=request.POST.get('nombre_cbr')
        cbr.correo_cbr= request.POST.get('correo_cbr')
        cbr.telefono = request.POST.get('telefono')
        cbr.hor_atencion_id_horario = horario
        dir.nombre_calle = request.POST.get('calle')
        dir.numero_casa = request.POST.get ('numero_lugar')
        comuna = get_object_or_404(Comuna, pk= request.POST.get ('cmbComuna'))
        cbr.direccion_id_direccion = dir
        dir.comuna_id_comuna = comuna
        dir.save()
        
        
        if cbr.nombre_cbr == "" or cbr.correo_cbr == "" or cbr.telefono == "" or cbr.hor_atencion_id_horario == "" or dir.nombre_calle == "" or dir.numero_casa == "":
            messages.warning(request, 'Los campos no pueden quedar vacios.')
            return redirect('registrarse')
        else:
            try:
                cbr.save()   
                messages.success(request, "CBR creado correctamente")
                return redirect(to="agregarCbr")
            except Exception as e:
                mensaje = "No se ha podido guardar el cbr: " + str(e)
                messages.warning(request, mensaje)
    


    data={
        'comuna': comuna,
        'cbr' : cbr,
        'provincia': provincia,
        'aten': aten,
    
    }


    return render(request, 'templates/agregar_cbr.html',data)

@login_required(login_url='/iniciar_sesion')
def listar_cbr (request):
    cbr = Cbr.objects.raw("SELECT CBR.id_cbr, CBR.nombre_cbr, CBR.correo_cbr, CBR.telefono, concat(DIRECCION.nombre_calle, ' ', DIRECCION.numero_casa) AS DIR_CBR , concat(HOR_ATENCION.dias_atencion ,' ', HOR_ATENCION.horario_apertura ,'-', HOR_ATENCION.horario_cierre) AS HORARIOS FROM UNIONLINE.CBR JOIN UNIONLINE.DIRECCION ON CBR.DIRECCION_id_direccion = DIRECCION.id_direccion JOIN HOR_ATENCION ON CBR.HOR_ATENCION_id_horario = HOR_ATENCION.id_horario " )
    data={
        'cbr' : cbr
    }
    return render(request, 'templates/listar_cbr.html' , data)

@login_required(login_url='/iniciar_sesion')
def modificar_cbr(request,id):
    comuna = Comuna.objects.all()
    cbr = Cbr.objects.raw ("SELECT * FROM UNIONLINE.CBR JOIN UNIONLINE.DIRECCION ON UNIONLINE.CBR.DIRECCION_id_direccion = UNIONLINE.DIRECCION.id_direccion JOIN UNIONLINE.HOR_ATENCION ON UNIONLINE.CBR.HOR_ATENCION_id_horario = UNIONLINE.HOR_ATENCION.id_horario JOIN UNIONLINE.COMUNA ON UNIONLINE.DIRECCION.COMUNA_id_comuna = UNIONLINE.COMUNA.id_comuna where UNIONLINE.CBR.id_cbr = %s;" ,[id])
    

    data={
        'cbr':cbr,
        'comuna': comuna,
    }
    
    if request.method == 'POST':
        
        cbr=Cbr()
        dir = Direccion()
        horario= get_object_or_404(HorAtencion, pk= request.POST.get ('cmbHorario'))
        cbr.nombre_cbr=request.POST.get('nombre_cbr')
        cbr.correo_cbr= request.POST.get('correo_cbr')
        cbr.telefono = request.POST.get('telefono')
        cbr.hor_atencion_id_horario = horario
        dir.nombre_calle = request.POST.get('calle')
        dir.numero_casa = request.POST.get ('numero_lugar')
        comuna = get_object_or_404(Comuna, pk= request.POST.get ('cmbComuna'))
        cbr.direccion_id_direccion = dir
        dir.comuna_id_comuna = comuna
        dir.save()
        
        
        if cbr.nombre_cbr == "" or cbr.correo_cbr == "" or cbr.telefono == "" or cbr.hor_atencion_id_horario == "" or dir.nombre_calle == "" or dir.numero_casa == "":
            messages.warning(request, 'Los campos no pueden quedar vacios.')
            return redirect('registrarse')
        else:
            try:
                cbr.save()   
                messages.success(request, "CBR Modificado Correctamente")
                return redirect(to="modificarCbr")
            except Exception as e:
                mensaje = "No se ha podido guardar el cbr: " + str(e)
                messages.warning(request, mensaje)
    return render(request,'templates/modificar_cbr.html', data)

@login_required(login_url='/iniciar_sesion')
def eliminar_cbr(request,id):
    cbr = get_object_or_404(Cbr, id_cbr = id)
    cbr.delete()
    messages.success(request, "Eliminado correctamente")
    return redirect(to="listarCbr")

@login_required(login_url='/iniciar_sesion')
def regDirector(request):
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

            tipoU.id_tipou = 2

            usuario.cbr_id_cbr = cbr
            usuario.t_usuario_id_tipou = tipoU

            
            

            if usuario.rut_usuario == "" or usuario.primer_nombre == "" or usuario.segundo_nombre == "" or usuario.primer_apellido == "" or usuario.segundo_apellido == "" or usuario.telefono == "" or usuario.correo_electronico == "" or usuario.contrasenna == "":
                messages.warning(request, 'Los campos no pueden quedar vacios.')
                return redirect('registrarse')
            else:
                try: 
                    usuario.contrasenna = request.POST.get('Contraseña')
                    usuario.save()
                    messages.success(request, "Cuenta Creada Correctamente")
                    return redirect(to="iniciar_sesion")
                except Exception as e:
                    mensaje = "No se ha podido guardar el usuario: " + str(e)
                    messages.warning(request, mensaje)

        
    return render(request, 'registration/registrar_usuarios.html')

@login_required(login_url='/iniciar_sesion')
def listarDirector (request):
    #tuser = get_object_or_404(TUsuario, pk = id_tipoU)
    #tuser = TUsuario.objects.filter(id_tipoU = 2)
    user = Usuario.objects.filter(t_usuario_id_tipou = 2)
    #user = Usuario.objects.raw("SELECT rut_usuario, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, telefono, correo_electronico FROM UNIONLINE.USUARIO where T_USUARIO_id_tipoU = %s;",[tuser])
    #tuser = TUsuario.objects.all()
    #usuario = Usuario.objects.raw("SELECT * FROM UNIONLINE.USUARIO where T_USUARIO_id_tipoU = 5 and rut_usuario = %s;",[])
    return render(request, 'templates/listar_usuarios.html',{"Usuario": user})

@login_required(login_url='/iniciar_sesion')
def Eliminardirector (request, id):
    director=Usuario.objects.get(id_usuario = id)
    director.delete()

    return redirect('listarDirector')

@login_required(login_url='/iniciar_sesion')
def EditarDirector (request, id):
    director=Usuario.objects.get(id_usuario = id)
    
    data = {
        'director' : director
    }

    return render(request, "editar_director.html", data)

@login_required(login_url='/iniciar_sesion')
def ediciondirector (request):
        #usuario.primer_nombre = request.POST.get('Nombre')
    id = int(request.POST['id'])
    nombre = request.POST['Nombre']
        #usuario.segundo_nombre = request.POST.get('Segundo_Nombre')
    sdo_nombre = request.POST['Segundo_Nombre']
        #usuario.primer_apellido = request.POST.get('Primer_Apellido')
    apellido = request.POST['Primer_Apellido']
        #usuario.segundo_apellido = request.POST.get('Segundo_Apellido')
    sdoapellido = request.POST['Segundo_Apellido']
        #usuario.telefono = request.POST.get('Telefono')
    fono = request.POST['Telefono']
        #usuario.correo_electronico = request.POST.get('Correo')
    email = request.POST['Correo']
        #usuario.contrasenna = request.POST.get('Contraseña')
    pwd = request.POST['c']
    #director = get_object_or_404(Usuario, id_usuario = id)
    director=Usuario.objects.get(id_usuario = id)
    director.primer_nombre = nombre
    director.segundo_nombre = sdo_nombre
    director.primer_apellido = apellido
    director.segundo_apellido = sdoapellido
    director.telefono = fono
    director.correo_electronico = email
    director.contrasenna = pwd

    director.save()

    return redirect('listarDirector')

@login_required(login_url='/iniciar_sesion')
def carrito_pagar(request):
    #agregar a todas las ventanas de cliente
    tramites = listar_tramites()
    usu = request.user
    list_carrito = listar_carrito(usu.username)
    valor=0
    can_carrito = 0
    for i in list_carrito:
        valor += int(i.valor_tramite)
        can_carrito += 1
    miles_translator = str.maketrans(".,", ",.")
    valor = "{:,}".format(valor).translate(miles_translator)
    ######
    data={

    'carrito': list_carrito,
    'tramites': tramites,
    'valor': valor,
    'can_carrito': can_carrito,
    }

    return render(request, 'templates/carrito_pagar.html', data)
#De aquí empieza transbank y muere todo

#transferencia
def transferencia(request):
    return render (request, 'templates/transferencia.html')

#web pay 
global devolver_token
def devolver_token(): 
    return response.token

#crear transaccion
def voucher(request):
    token_recibido= devolver_token()
    return render(request, 'Web_pay_plus/commit.html')

def webpay_plus_create(request):
    buy_order=""
    session_id=""
    amount=0
    user = request.user
    carrito_llenar = listar_carrito(user.username)
    total=0
    for i in carrito_llenar:
       total += int(i.valor_tramite)
    if request.method == 'GET':
        print("Webpay Plus Transaction.create")
        buy_order = "RB" + str(random.randrange(1000000, 99999999))
        session_id = str(random.randrange(1000000, 99999999))
        amount = total
    #return_url = "http://transbank-rest-demo.herokuapp.com/webpay_plus/commit"
    return_url= "http://127.0.0.1:8000/commit/"

    create_request = {
        "buy_order": buy_order,
        "session_id": session_id,
        "amount": amount,
        "return_url": return_url
    }
    tx = Transaction()
    print("antes de crear")
    response = tx.create(buy_order, session_id, amount, return_url)
    print("despues de crear")
    
    token=response['token']
    data = {
        'token1':token
    }

    print(response)
    
    return render(request,'Web_pay_plus/create.html', data )

#commit de la compra

def webpay_plus_commit(request):
    token = request.GET.get("token_ws")
    tx = Transaction()
    response = tx.commit(token=token)
    monto = response['amount']
    status = response['status']
    buy_order = response['buy_order']
    card_detail = response['card_detail']
    card_number = card_detail['card_number']
    date = response['transaction_date']
    date = date.replace("T", " ")
    date = date[:-5]
    print("response: {}".format(response))
    user = request.user
    carrito_llenar = listar_carrito(user.username)
    if status == "AUTHORIZED":
        tpago = get_object_or_404(TipoPago, pk=1)
        for i in carrito_llenar:
            car = get_object_or_404(CarCompra, pk=i.id_carrito)
            estado_pago = EstadoPago()
            estado_pago.car_compra_id_carrito = car
            estado_pago.tipo_pago_id_tipop = tpago
            estado_pago.n_boleta = buy_order
            estado_pago.save()
            soli = car.solicitud_id_soli
            solicitud = get_object_or_404(Solicitud, pk = soli.id_soli)
            now = datetime.now()
            solicitud.fecha_solicitud = now.strftime("%Y-%m-%d %H:%M:%S")
            solicitud.save()
    data = {
        'token':token, 
        'response':response,
        'monto': monto,
        'status': status,
        'buy_order': buy_order,
        'card_number': card_number,
        'date': date,
    }
    return render(request,'Web_pay_plus/commit.html',data)


#transaccion creada

def show_create(request):
    ##if request.method==('GET'):
    return render(request,'transaccion_completa/create.html')

#mandar transaccion creada
def send_create(request):
    if request.method==('POST'):

        buy_order = request.form.get('buy_order')
        session_id = request.form.get('session_id')
        amount = request.form.get('amount')
        card_number = request.form.get('card_number')
        cvv = request.form.get('cvv')
        card_expiration_date = request.form.get('card_expiration_date')
        resp = Transaction.create(
            buy_order=buy_order, session_id=session_id, amount=amount,
            card_number=card_number, cvv=cvv, card_expiration_date=card_expiration_date
        )

    return render(request,'transaccion_completa/created.html', resp=resp, req=request.form.values, dt=dt)