   M S B A M L   `   `   `  ���� ^  ZCronomur Reader Interface, Version=2016.2.6115.32835, Culture=neutral, PublicKeyToken=null+clr-namespace:Cronomur_WRICronomur_WRI       Cronomur_WRI.ConfigCarrera   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ,localclr-namespace:Cronomur_WRI       N� Title$  ConfigCarrera��.���� $
��Auto=�.���� .+��� $��RunScore Server��$	��Top=�$��12q�$��
12,12,12,6q�.���� .+��  � RowDefinitions �� �� �� �� ��  � ColumnDefinitions �� $��5* ��� .+��� $��0��$��0��$
��12 6q�$
��Bold"�.��Dirección IP:�� -    ��Name$ rs_ip��$��1��$��0��$
��12 0q�$	��4 6q�{�.  $��	127.0.0.1���� $��0��$��1��$
��12 6q�$
��Bold"�.��	Puerto:�� -   $ rs_port��$��1��$��1��$
��12 0q�$	��4 6q�{�.  $
��7000���� $��2��$��2��$
��12 6q�$
��Bold"�.��	Evento:�� -   $ rs_event_name��$��3��$��2��$
��12 0q�$	��4 6q�{�.  �� -    ��Name$ use_runscore��$��4��$��0��$��12,12,12,12q�$
��4 -1q� d� 	IsChecked$ FalseY�.��Usar un servidor de RunScore�� $��	Conexión��$	��Top=�$��12q�$��
12,12,12,6q�.���� .+��� $
��12 6q�$
��Bold"�.��%#Tiempo de espera (en milisegundos):�� -   $ rs_conn_timeout��$
��12 0q�$	��4 6q�$
��5000����  �� Padding$
 12 6q����	#FF999999$��12(�$
��Wrap=�$��Italic$� �� TextAlignment$ Justify=�HFEste es el tiempo de espera para la conexión al servidor de RunScore.�� �� �� .�
Consejo:ge Si el servidor de RunScore no está en un servidor remoto, considere definir un valor más pequeño.�� �� Ej: 5000 (5 segundos)