   M S B A M L   `   `   `  ����^  ZCronomur Reader Interface, Version=2016.1.6114.36007, Culture=neutral, PublicKeyToken=null+clr-namespace:Cronomur_WRICronomur_WRI  5           Cronomur_WRI.ConfigCarrera   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Cronomur_WRI     5        N� Title$  ConfigCarrera��5	      .��5      �� $
��Auto=�6   .��5      �� .+�5      �� $��RunScore Server��6   $	��Top=�6'   $��12q�6?   $��
12,12,12,6q�6L   .��5      �� .+�5      �  � RowDefinitions 5      �� 5   	   �� 5   	   �� 5   	   �� 5   	   �� 5   	    � ColumnDefinitions 5      �� 5   	   $��5* �6   �� 5   	   .+�5      �� $��0��6   $��0��6   $
��12 6q�6+   $
��Bold"�6:   .��6L   Dirección IP:6[   �� 5      -   5         ��Name$ rs_ip��5   -   $��1��6   $��0��6   $
��12 0q�6<   $	��4 6q�6J   {�.  $��	127.0.0.1��6j   6{   �� 5      $��0��6   $��1��6   $
��12 6q�6+   $
��Bold"�6:   .��6L   	Puerto:6U   �� 5      -   5        $ rs_port��5   -   $��1��6   $��1��6   $
��12 0q�6>   $	��4 6q�6L   {�.  $
��7000��6l   6x   �� 5       $��2��6   $��2��6   $
��12 6q�6/   $
��Bold"�6>   .��6P   	Evento:6Y   �� 5!      -   5        $ rs_event_name��5!   1   $��3��6   $��2��6   $
��12 0q�6H   $	��4 6q�6V   {�.  6v   �� 5"      -   5         ��Name$ use_runscore��5"      $��4��6'   $��0��64   $��12,12,12,12q�6D   $
��4 -1q�6Y    d� 	IsChecked$ FalseY�6h   .��6�   Usar un servidor de RunScore6�   5#      5$      5%      �� 5&      $��	Conexión��6   $	��Top=�6    $��12q�68   $��
12,12,12,6q�6E   .��5'      �� .+�5(      �� $
��12 6q�6   $
��Bold"�6   .��6.   %#Tiempo de espera (en milisegundos):6S   �� 5)      -   5        $ rs_conn_timeout��5)      $
��12 0q�6(   $	��4 6q�66   $
��5000��6D   6P   �� 5*       �� Padding$
 12 6q�6   ���	#FF999999$��12(�67   $
��Wrap=�6E   $��Italic$�6Y    �� TextAlignment$ Justify=�6l   HFEste es el tiempo de espera para la conexión al servidor de RunScore.�� 5,      �� 5-      �� 5.      .�6   
Consejo:6   ge Si el servidor de RunScore no está en un servidor remoto, considere definir un valor más pequeño.�� 5/      �� 50      Ej: 5000 (5 segundos)52      53      54      55      56      57      