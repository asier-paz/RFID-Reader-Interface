   M S B A M L   `   `   `  ���� ^  ZCronomur Reader Interface, Version=2016.2.6115.32835, Culture=neutral, PublicKeyToken=null+clr-namespace:Cronomur_WRICronomur_WRI       Cronomur_WRI.Configuracion   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:Cronomur_WRI       N� Title$  Configuracion�� � 	Resources    Link �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value��%  &	        �� " ��.���  � ��$ Center=���  � ��$ Center=���  �@�Cursor$
 Hand����  � ��
 ��  ���  � ��
 ���� ��  � �� �� " ��.R��� $��	Underline{�#��z��#��z�� P�  � �� . . ���  � ��
 �  ��.��� .+��� $
��Auto=�.���� .+��� $��Lectura��$	��Top=�$��12q�$��
12,12,12,6q�.���� .+��� $
��12 6q�$
��Bold"�.��42Tiempo de espera entre lecturas (en milisegundos):�� -   	 ��Name$	 reader_read_timeout��$
��12 0q�$	��4 6q��� 
 �� Padding$

 12 6q����	#FF999999$��12(�$
��Wrap=�$��Italic$� �� TextAlignment$ Justify=���Este es el tiempo que pasa entre lectura y lectura. Esto quiere decir que si tenemos un tiempo de espera de 100 milisegundos, se harán 10 lecturas por segundo.�� �� �� .�Advertencia:�� Si el valor es muy bajo (por ejemplo menor que 50) no dará tiempo a que el lector lea antes de mandar la petición de volver a leer. Esto ocasionaría una posible pérdida de datos de lectura haciendo así que algunos tiempos registrados no sean precisos.�� �� Ej: 100 (0.1 segundos)�� $��Configuración de antena��$	��Top=�$��12q�$��
12,12,12,6q�.���� .+��� $
��12 6q�$
��Bold"�.��Atenuación:�� -    ��Name$ reader_attenuation��$
��12 0q�$	��4 6q�.@��� -   $��atten_0��
 � Tag$ 0�� �� 
IsSelected . .��0 dB�� -   $��atten_2��$ 1��.��-2 dB�� -   $��atten_4��$ 2��.��-4 dB�� -   $��atten_6��$ 3��.��-6 dB�� -   $��atten_8��$ 4��.��-8 dB�� -   $��atten_10��$ 5��.��-10 dB�� -	   $��atten_12��$ 6��.��-12 dB�� -
   $��atten_14��$ 7��.��-14 dB�� -   $��atten_16��$ 8��.��-16 dB�� -   $��atten_18��$ 9��.��-18 dB�� -   $��atten_20��$ 10��.��-20 dB�� -   $��atten_22��$ 11��.��-22 dB�� -   $��atten_24��$ 12��.��-24 dB�� -   $��atten_26��$ 13��.��-26 dB�� -   $��atten_28��$ 14��.��-28 dB�� -   $��atten_30��$ 15��.��-30 dB�� -    ��Name$ reader_save_atten_btn��$��12q�$	��4 6q�$
��Bold"�.��	Guardar�� -   $ atten_table_btn��$��Center=�#��[  .��.,Ver tabla de distancia según la atenuación�� $"��Información del dispositivo��$	��Top=�$��12q�$��
12,6,12,12q�.���� .+��� $
��12 6q�$
��Bold"�.��Nombre del dispositivo:�� -   $	 reader_name��$
��12 0q�$	��4 6q� �� 	MaxLength$ 8���� $
��12 6q�$
��Bold"�.��	Serial:�� -   $	 reader_serial��$
��12 0q�$	��4 6q�{�.  �� $
��12 6q�$
��Bold"�.��Tipo:�� -   $	 reader_type��$
��12 0q�$	��4 6q�{�.  �� $
��12 6q�$
��Bold"�.��Versión de Software:�� -   $	 reader_software_version��$
��12 0q�$	��4 6q�{�.  �� $
��12 6q�$
��Bold"�.��Versión de Hardware:�� -   $	 reader_hardware_version��$
��12 0q�$	��4 6q�{�.  �� -   $ reader_save_name_btn��$��12q�$	��4 6q�$
��Bold"�.��	Guardar