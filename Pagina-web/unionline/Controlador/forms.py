from django import forms
from .models import FormFormulario

class FormFormularioForm(forms.ModelForm):
    class Meta:
        model = FormFormulario
        fields = '__all__'


    