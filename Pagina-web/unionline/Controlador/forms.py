from django import forms
from .models import FormFormulario, Cbr

class FormFormularioForm(forms.ModelForm):
    class Meta:
        model = FormFormulario
        fields = '__all__'


class CbrForm (forms.ModelForm):
    class Meta:
        model= Cbr
        fields = '__all__'

    