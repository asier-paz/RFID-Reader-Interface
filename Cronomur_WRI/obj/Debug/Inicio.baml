   M S B A M L   `   `   `  ����^  ZCronomur Reader Interface, Version=2016.1.6114.36007, Culture=neutral, PublicKeyToken=null+clr-namespace:Cronomur_WRICronomur_WRI  5          Cronomur_WRI.Inicio   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:Cronomur_WRI     5        N� Title$  Inicio��5	      .��5      �  � ColumnDefinitions 5      �� 5      $	��140 �6   �� 5      .+�5      �� -   5         ��Name$ 
events_box��5      $��0��6    $��1��6-   6?   �� 5      $��0��6   $��0��6   .+�5      �� $
��12 6q�6   $
��Bold"�6   .��6,   Dirección IP:6;   �� 5      -   5         ��Name$ conn_ip��5      $
��12 0q�6   $	��4 6q�6,   6\   �� 5      $��	12,8,12,6q�6   $
��Bold"�6   .��61   	Puerto:6:   �� 5      -   5        $ 	conn_port��5      $
��12 0q�6    $	��4 6q�6.   6V   �� 5      -   5         ��Name$ connect_btn��5      $��12q�6!   $	��4 6q�6-   $
��Bold"�6;   .��6g   
Conectar6q   �� 5      -   5        $ read_btn��5      $��
12,0,12,12q�6   $	��4 6q�62   $
��Bold"�6@   {�.  .��6{   Empezar lectura6�   �� 5      -   5        $ config_dump_btn��5      $��
12,0,12,12q�6%   $	��4 6q�69   $
��Bold"�6G   .��6w   Mostrar config.6�   �� 5      -   5         ��Name$ sound��5      $��
12,0,12,12q�6   $
��4 -1q�61   .��6t   Emitir sonidos6�   5      5      5      