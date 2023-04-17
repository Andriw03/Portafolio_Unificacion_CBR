from django.db import models


class BPago(models.Model):
    id_boleta = models.AutoField(primary_key=True)
    fecha_emision = models.DateTimeField()
    monto_pago = models.IntegerField()
    tipo_pago = models.IntegerField()
    nombre_conservador = models.IntegerField()

    class Meta:
        managed = False
        db_table = 'B_PAGO'


class CarCompra(models.Model):
    id_carrito = models.AutoField(primary_key=True)
    estado = models.IntegerField()
    solicitud_id_soli = models.ForeignKey('Solicitud',on_delete=models.CASCADE, db_column='SOLICITUD_id_soli')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'CAR_COMPRA'
        unique_together = (('id_carrito', 'solicitud_id_soli'),)


class Cbr(models.Model):
    id_cbr = models.AutoField(primary_key=True)
    nombre_cbr = models.CharField(max_length=100)
    correo_cbr = models.CharField(max_length=50)
    telefono = models.CharField(max_length=12)
    hor_atencion_id_horario = models.ForeignKey('HorAtencion', models.DO_NOTHING, db_column='HOR_ATENCION_id_horario')  # Field name made lowercase.
    direccion_id_direccion = models.ForeignKey('Direccion', models.DO_NOTHING, db_column='DIRECCION_id_direccion')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'CBR'
        unique_together = (('id_cbr', 'hor_atencion_id_horario', 'direccion_id_direccion'),)


class ClasProp(models.Model):
    id_clas = models.AutoField(primary_key=True)
    foja = models.IntegerField()
    numero = models.IntegerField()
    anno = models.DateTimeField()
    razon_social = models.CharField(max_length=45, blank=True, null=True)
    rut_empresa = models.CharField(max_length=15, blank=True, null=True)

    class Meta:
        managed = False
        db_table = 'CLAS_PROP'


class Comuna(models.Model):
    id_comuna = models.IntegerField(primary_key=True)
    nombre_comuna = models.CharField(max_length=45)
    provincia_id_provincia = models.ForeignKey('Provincia', models.DO_NOTHING, db_column='PROVINCIA_id_provincia')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'COMUNA'
        unique_together = (('id_comuna', 'provincia_id_provincia'),)


class Direccion(models.Model):
    id_direccion = models.AutoField(primary_key=True)
    nombre_calle = models.CharField(max_length=45)
    numero_casa = models.IntegerField()
    comuna_id_comuna = models.ForeignKey(Comuna, models.DO_NOTHING, db_column='COMUNA_id_comuna')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'DIRECCION'
        unique_together = (('id_direccion', 'comuna_id_comuna'),)


class Documento(models.Model):
    id_documento = models.AutoField(primary_key=True)
    nombre_doc = models.CharField(max_length=45)
    doc = models.TextField(blank=True, null=True)
    solicitud_id_soli = models.ForeignKey('Solicitud', models.DO_NOTHING, db_column='SOLICITUD_id_soli')  # Field name made lowercase.
    tipo_documento_id_tipodoc = models.ForeignKey('TipoDocumento', models.DO_NOTHING, db_column='TIPO_DOCUMENTO_id_tipodoc')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'DOCUMENTO'
        unique_together = (('id_documento', 'solicitud_id_soli', 'tipo_documento_id_tipodoc'),)


class DuennoProp(models.Model):
    id_duenno = models.AutoField(primary_key=True)
    rut_duenno = models.CharField(max_length=15)
    primer_nombre = models.CharField(max_length=45)
    segundo_nombre = models.CharField(max_length=45)
    primer_apellido = models.CharField(max_length=45)
    segundo_apellido = models.CharField(max_length=45)
    correo_electronico = models.CharField(max_length=50)
    telefono = models.CharField(max_length=12)

    class Meta:
        managed = False
        db_table = 'DUENNO_PROP'


class Error(models.Model):
    id_error = models.AutoField(primary_key=True)
    codigo_error = models.CharField(max_length=45)
    mensaje_error = models.CharField(max_length=100)
    fecha_error = models.DateTimeField()

    class Meta:
        managed = False
        db_table = 'ERROR'


class EstadoPago(models.Model):
    id_estado = models.AutoField(primary_key=True)
    n_boleta = models.CharField(max_length=45)
    car_compra_id_carrito = models.ForeignKey(CarCompra, models.DO_NOTHING, db_column='CAR_COMPRA_id_carrito')  # Field name made lowercase.
    tipo_pago_id_tipop = models.ForeignKey('TipoPago', models.DO_NOTHING, db_column='TIPO_PAGO_id_tipoP')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'ESTADO_PAGO'
        unique_together = (('id_estado', 'car_compra_id_carrito', 'tipo_pago_id_tipop'),)


class FormFormulario(models.Model):
    id_formulario = models.AutoField(primary_key=True)
    nombre_form = models.CharField(max_length=45)
    telefono = models.CharField(max_length=45)
    correo_form = models.CharField(max_length=45)
    asunto_form = models.CharField(max_length=45)
    detalle_form = models.CharField(max_length=500)
    estado = models.CharField(max_length=45, blank=True, null=True)
    usuario_id_usuario = models.ForeignKey('Usuario', models.DO_NOTHING, db_column='USUARIO_id_usuario')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'FORMULARIO'


class HorAtencion(models.Model):
    id_horario = models.AutoField(primary_key=True)
    dias_atencion = models.CharField(max_length=45)
    horario_apertura = models.CharField(max_length=10)
    horario_cierre = models.CharField(max_length=10)

    class Meta:
        managed = False
        db_table = 'HOR_ATENCION'


class Propiedad(models.Model):
    id_propiedad = models.AutoField(primary_key=True)
    descripcion = models.CharField(max_length=100)
    direccion_id_direccion = models.ForeignKey(Direccion, models.DO_NOTHING, db_column='DIRECCION_id_direccion')  # Field name made lowercase.
    tipo_propiedad_id_tipop = models.ForeignKey('TipoPropiedad', models.DO_NOTHING, db_column='TIPO_PROPIEDAD_id_tipoP')  # Field name made lowercase.
    clas_prop_id_clas = models.ForeignKey(ClasProp, models.DO_NOTHING, db_column='CLAS_PROP_id_clas')  # Field name made lowercase.
    duenno_prop_id_duenno = models.ForeignKey(DuennoProp, models.DO_NOTHING, db_column='DUENNO_PROP_id_duenno')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'PROPIEDAD'
        unique_together = (('id_propiedad', 'direccion_id_direccion', 'tipo_propiedad_id_tipop', 'clas_prop_id_clas', 'duenno_prop_id_duenno'),)


class Provincia(models.Model):
    id_provincia = models.IntegerField(primary_key=True)
    nombre_provincia = models.CharField(max_length=45)
    region_id_region = models.ForeignKey('Region', models.DO_NOTHING, db_column='REGION_id_region')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'PROVINCIA'
        unique_together = (('id_provincia', 'region_id_region'),)


class Region(models.Model):
    id_region = models.IntegerField(primary_key=True)
    nombre_region = models.CharField(max_length=45)

    class Meta:
        managed = False
        db_table = 'REGION'


class Solicitud(models.Model):
    id_soli = models.AutoField(primary_key=True)
    fecha_solicitud = models.DateTimeField()
    fecha_cierre = models.DateTimeField()
    estado = models.CharField(max_length=45)
    numero_seguimiento = models.CharField(max_length=45)
    comentario = models.CharField(db_column='Comentario', max_length=100, blank=True, null=True)  # Field name made lowercase.
    usuario_id_usuario = models.ForeignKey('Usuario', models.DO_NOTHING, db_column='USUARIO_id_usuario')  # Field name made lowercase.
    propiedad_id_propiedad = models.ForeignKey(Propiedad, models.DO_NOTHING, db_column='PROPIEDAD_id_propiedad')  # Field name made lowercase.
    tramite_id_tramite = models.ForeignKey('Tramite', models.DO_NOTHING, db_column='TRAMITE_id_tramite')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'SOLICITUD'
        unique_together = (('id_soli', 'usuario_id_usuario', 'propiedad_id_propiedad', 'tramite_id_tramite'),)


class TipoDocumento(models.Model):
    id_tipodoc = models.IntegerField(primary_key=True)
    tipo_doc = models.CharField(max_length=45)

    class Meta:
        managed = False
        db_table = 'TIPO_DOCUMENTO'


class TipoPago(models.Model):
    id_tipop = models.IntegerField(db_column='id_tipoP', primary_key=True)  # Field name made lowercase.
    nombre_tipop = models.CharField(db_column='nombre_tipoP', max_length=45)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'TIPO_PAGO'


class TipoPropiedad(models.Model):
    id_tipop = models.IntegerField(db_column='id_tipoP', primary_key=True)  # Field name made lowercase.
    nombre_tipop = models.CharField(db_column='nombre_tipoP', max_length=45)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'TIPO_PROPIEDAD'


class Tramite(models.Model):
    id_tramite = models.AutoField(primary_key=True)
    nombre_tramite = models.CharField(max_length=100)
    valor_tramite = models.CharField(max_length=45)
    estado = models.CharField(max_length=45)
    descripcion = models.CharField(max_length=250)
    t_tramite_id_tipot = models.ForeignKey('TTramite', models.DO_NOTHING, db_column='T_TRAMITE_id_tipoT')  # Field name made lowercase.
    t_documento = models.CharField(max_length=250)

    class Meta:
        managed = False
        db_table = 'TRAMITE'
        unique_together = (('id_tramite', 't_tramite_id_tipot'),)


class TPago(models.Model):
    id_tipop = models.IntegerField(db_column='id_tipoP', primary_key=True)  # Field name made lowercase.
    nombre_tipop = models.CharField(db_column='nombre_tipoP', max_length=45)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'T_PAGO'


class TTramite(models.Model):
    id_tipot = models.AutoField(db_column='id_tipoT', primary_key=True)  # Field name made lowercase.
    nombre_tipot = models.CharField(db_column='nombre_tipoT', max_length=45)  # Field name made lowercase.
    tipo_propiedad_id_tipop = models.ForeignKey(TipoPropiedad, models.DO_NOTHING, db_column='TIPO_PROPIEDAD_id_tipoP')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'T_TRAMITE'
        unique_together = (('id_tipot', 'tipo_propiedad_id_tipop'),)


class TUsuario(models.Model):
    id_tipou = models.IntegerField(db_column='id_tipoU', primary_key=True)  # Field name made lowercase.
    nombre_tipou = models.CharField(db_column='nombre_tipoU', max_length=45)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'T_USUARIO'


class Usuario(models.Model):
    id_usuario = models.AutoField(primary_key=True)
    rut_usuario = models.CharField(max_length=15)
    contrasenna = models.CharField(max_length=100)
    primer_nombre = models.CharField(max_length=45)
    segundo_nombre = models.CharField(max_length=45, blank=True, null=True)
    primer_apellido = models.CharField(max_length=45)
    segundo_apellido = models.CharField(max_length=45)
    correo_electronico = models.CharField(max_length=45)
    telefono = models.CharField(max_length=12)
    cbr_id_cbr = models.ForeignKey(Cbr, models.DO_NOTHING, db_column='CBR_id_cbr')  # Field name made lowercase.
    t_usuario_id_tipou = models.ForeignKey(TUsuario, models.DO_NOTHING, db_column='T_USUARIO_id_tipoU')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'USUARIO'
        unique_together = (('id_usuario', 't_usuario_id_tipou'),)


class AuthGroup(models.Model):
    name = models.CharField(unique=True, max_length=150)

    class Meta:
        managed = False
        db_table = 'auth_group'


class AuthGroupPermissions(models.Model):
    group = models.ForeignKey(AuthGroup, models.DO_NOTHING)
    permission = models.ForeignKey('AuthPermission', models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'auth_group_permissions'
        unique_together = (('group', 'permission'),)


class AuthPermission(models.Model):
    name = models.CharField(max_length=255)
    content_type = models.ForeignKey('DjangoContentType', models.DO_NOTHING)
    codename = models.CharField(max_length=100)

    class Meta:
        managed = False
        db_table = 'auth_permission'
        unique_together = (('content_type', 'codename'),)


class AuthUser(models.Model):
    password = models.CharField(max_length=128)
    last_login = models.DateTimeField(blank=True, null=True)
    is_superuser = models.IntegerField()
    username = models.CharField(unique=True, max_length=150)
    first_name = models.CharField(max_length=150)
    last_name = models.CharField(max_length=150)
    email = models.CharField(max_length=254)
    is_staff = models.IntegerField()
    is_active = models.IntegerField()
    date_joined = models.DateTimeField()

    class Meta:
        managed = False
        db_table = 'auth_user'


class AuthUserGroups(models.Model):
    user = models.ForeignKey(AuthUser, models.DO_NOTHING)
    group = models.ForeignKey(AuthGroup, models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'auth_user_groups'
        unique_together = (('user', 'group'),)


class AuthUserUserPermissions(models.Model):
    user = models.ForeignKey(AuthUser, models.DO_NOTHING)
    permission = models.ForeignKey(AuthPermission, models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'auth_user_user_permissions'
        unique_together = (('user', 'permission'),)


class DjangoAdminLog(models.Model):
    action_time = models.DateTimeField()
    object_id = models.TextField(blank=True, null=True)
    object_repr = models.CharField(max_length=200)
    action_flag = models.PositiveSmallIntegerField()
    change_message = models.TextField()
    content_type = models.ForeignKey('DjangoContentType', models.DO_NOTHING, blank=True, null=True)
    user = models.ForeignKey(AuthUser, models.DO_NOTHING)

    class Meta:
        managed = False
        db_table = 'django_admin_log'


class DjangoContentType(models.Model):
    app_label = models.CharField(max_length=100)
    model = models.CharField(max_length=100)

    class Meta:
        managed = False
        db_table = 'django_content_type'
        unique_together = (('app_label', 'model'),)


class DjangoMigrations(models.Model):
    app = models.CharField(max_length=255)
    name = models.CharField(max_length=255)
    applied = models.DateTimeField()

    class Meta:
        managed = False
        db_table = 'django_migrations'


class DjangoSession(models.Model):
    session_key = models.CharField(primary_key=True, max_length=40)
    session_data = models.TextField()
    expire_date = models.DateTimeField()

    class Meta:
        managed = False
        db_table = 'django_session'
