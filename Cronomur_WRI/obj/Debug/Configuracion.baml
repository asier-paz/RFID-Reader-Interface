   M S B A M L   `   `   `  ����^  ZCronomur Reader Interface, Version=2016.1.6114.36007, Culture=neutral, PublicKeyToken=null+clr-namespace:Cronomur_WRICronomur_WRI  5           Cronomur_WRI.Configuracion   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Cronomur_WRI     5        N� Title$  Configuracion��5	       � 	Resources 5         Link �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value��%�  &	        �� 5      " ��6   .�5      ��  � ��$ Center=�6)   �� 5       � ��$ Center=�6+   �� 5       �@�Cursor$
 Hand��6   �� 5       � ��
 ��  �6"   �� 5       � ��
 ���� 6"   �� 5       � �� 5      �� 5      " ��6   .R�5      �� $��	Underline{�6   #��z��5      #��z��5       5      P� 5   	    � �� . . �5   
   ��  � ��
 �  ��6'   5   
   5      5      5       5!      .��5#      � .+�5$      �� $
��Auto=�6   .��5%      �� .+�5&      �� $��Lectura��6   $	��Top=�6    $��12q�68   $��
12,12,12,6q�6E   .��5'      �� .+�5(      �� $
��12 6q�6   $
��Bold"�6   .��6/   42Tiempo de espera entre lecturas (en milisegundos):6c   �� 5)      -   5        	 ��Name$	 reader_read_timeout��5)      $
��12 0q�6-   $	��4 6q�6;   6k   �� 5*      
 �� Padding$

 12 6q�6   ���	#FF999999$��12(�68   $
��Wrap=�6F   $��Italic$�6Z    �� TextAlignment$ Justify=�6m   ��Este es el tiempo que pasa entre lectura y lectura. Esto quiere decir que si tenemos un tiempo de espera de 100 milisegundos, se harán 10 lecturas por segundo.�� 5-   	   �� 5.   	   �� 5/   	   .�6   Advertencia:6   �� Si el valor es muy bajo (por ejemplo menor que 50) no dará tiempo a que el lector lea antes de mandar la petición de volver a leer. Esto ocasionaría una posible pérdida de datos de lectura haciendo así que algunos tiempos registrados no sean precisos.�� 52   	   �� 53   	   Ej: 100 (0.1 segundos)55   	   56      57      �� 58      $��Configuración de antena��6   $	��Top=�60   $��12q�6H   $��
12,12,12,6q�6U   .��59      �� .+�5:      �� $
��12 6q�6   $
��Bold"�6   .��6/   Atenuación:6<   �� 5;      -   5         ��Name$ reader_attenuation��5;      $
��12 0q�6-   $	��4 6q�6;   .@�5<   	   �� -   5        $��atten_0��5<      
 � Tag$ 0��6%    �� 
IsSelected . .��6?   0 dB6E   �� 5=   	   -   5        $��atten_2��5=      $ 1��6%   .��6-   -2 dB64   �� 5>   	   -   5        $��atten_4��5>      $ 2��6%   .��6-   -4 dB64   �� 5?   	   -   5        $��atten_6��5?      $ 3��6%   .��6-   -6 dB64   �� 5@   	   -   5        $��atten_8��5@      $ 4��6%   .��6-   -8 dB64   �� 5A   	   -   5        $��atten_10��5A      $ 5��6&   .��6.   -10 dB66   �� 5B   	   -	   5        $��atten_12��5B      $ 6��6&   .��6.   -12 dB66   �� 5C   	   -
   5        $��atten_14��5C      $ 7��6&   .��6.   -14 dB66   �� 5D   	   -   5        $��atten_16��5D      $ 8��6&   .��6.   -16 dB66   �� 5E   	   -   5        $��atten_18��5E      $ 9��6&   .��6.   -18 dB66   �� 5F   	   -   5        $��atten_20��5F      $ 10��6&   .��6/   -20 dB67   �� 5G   	   -   5        $��atten_22��5G      $ 11��6&   .��6/   -22 dB67   �� 5H   	   -   5        $��atten_24��5H      $ 12��6&   .��6/   -24 dB67   �� 5I   	   -   5        $��atten_26��5I      $ 13��6&   .��6/   -26 dB67   �� 5J   	   -   5        $��atten_28��5J      $ 14��6&   .��6/   -28 dB67   �� 5K   	   -   5        $��atten_30��5K      $ 15��6&   .��6/   -30 dB67   5L   	   �� 5M      -   5         ��Name$ reader_save_atten_btn��5M      $��12q�6.   $	��4 6q�6:   $
��Bold"�6H   .��6~   	Guardar6�   �� 5N      -   5        $ atten_table_btn��5N      $��Center=�6(   #��[  6E   .��6�   .,Ver tabla de distancia según la atenuación6�   5O      5P      �� 5Q      $"��Información del dispositivo��6   $	��Top=�64   $��12q�6L   $��
12,6,12,12q�6Y   .��5R      �� .+�5S      �� $
��12 6q�6   $
��Bold"�6   .��6/   Nombre del dispositivo:6H   �� 5T      -   5        $	 reader_name��5T      $
��12 0q�6%   $	��4 6q�63    �� 	MaxLength$ 8��6A   �� 5U      $
��12 6q�6   $
��Bold"�6   .��6/   	Serial:68   �� 5V      -   5        $	 reader_serial��5V      $
��12 0q�6'   $	��4 6q�65   {�.  6C   �� 5W      $
��12 6q�6   $
��Bold"�6   .��6/   Tipo:66   �� 5X      -   5        $	 reader_type��5X      $
��12 0q�6%   $	��4 6q�63   {�.  6A   �� 5Y      $
��12 6q�6   $
��Bold"�6   .��6/   Versión de Software:6E   �� 5Z      -   5        $	 reader_software_version��5Z      $
��12 0q�61   $	��4 6q�6?   {�.  6M   �� 5[      $
��12 6q�6   $
��Bold"�6   .��6/   Versión de Hardware:6E   �� 5\      -   5        $	 reader_hardware_version��5\      $
��12 0q�61   $	��4 6q�6?   {�.  6M   �� 5]      -   5        $ reader_save_name_btn��5]      $��12q�6-   $	��4 6q�69   $
��Bold"�6G   .��6|   	Guardar6�   5^      5_      5`      5a      5b      5c      