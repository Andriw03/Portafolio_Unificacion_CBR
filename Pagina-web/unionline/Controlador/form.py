from . models import Cbr, ClasProp, DuennoProp, Usuario, TUsuario,Comuna,Region,Provincia,Solicitud,Tramite
from django.forms import *

class TestForm(Form):
    regiones = ModelChoiceField(queryset=Region.objects.all(), widget=Select(attrs={
        'class': 'form-control'
    }))

    provincias = ModelChoiceField(queryset=Provincia.objects.none(), widget=Select(attrs={
        'class': 'form-control'
    }))

    comunas = ModelChoiceField(queryset=Comuna.objects.none(), widget=Select(attrs={
        'class': 'form-control'
    }))