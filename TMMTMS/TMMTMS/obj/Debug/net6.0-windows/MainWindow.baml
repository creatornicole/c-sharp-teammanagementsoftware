   M S B A M L   `   `   `  ����
  TMMTMS    TMMTMS.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $	��720��5   	   $
��1080��6     � WindowStartupLocation$  CenterScreen=�6#    � WindowStyle$
 None=�6H    � AllowsTransparency$
 True��5   	   
������  � 	Resources 5	         	BoolToVis��%   &	        �� 5
   
   6'   5      .��5      �� -   5        ���#FFFFFF �� CornerRadius$ 15��5   "   ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5      $��* �6#   .+�5      �  � RowDefinitions 5      �� 5      $��* �6$   �� 5      $��* �6$   .+�5      �� ���#E6E6E6$ 	10 10 0 0��6.   $��0,80,0,0q�6G   $��2��6Y   ._�5      ]� .+�5      �� $	��150��6!   $��80��6-    �� Padding$ 10q�69   $	��Top=�5      $��3,-65,-3,95q�62   ._�5       �� ��5!   "   �� 5"   &    �� ImageSource$h bD:\Stuff\Studium\HSMW\IF21wS1-B\4_c#\pruefungsbeleg\TMMTMS\TMMTMS\Images\TMM_Electric_Original.png��61   5$      5%      �� 5(      $��25,-65,25,95q�6$   $	��Top=�6:   $	��0.8��6O   ���#3333336\   �� 5+      $	��Top=�6%   .+�5,      ��   
menuButton#��[ 6%   .��5-   "   �� 	 �� Orientation$	 
Horizontal=�6-   .+�5/   &   �� $
��Team��60   50   #   51      �� 52      -   5        #��[ 52   O   .��53   "   �� $	 
Horizontal=�6-   .+�55   &   �� $��Anwesenheitsliste��60   56   #   57      �� 58      -   5        #��[ 58   I   .��59   "   �� $	 
Horizontal=�6-   .+�5;   &   �� $��	Protokoll��60   5<   #   5=      5>      5?      5@      5A      � 5D      $��1��6   $��20 70 20 20q�6#    5E      �� 5F      $
��auto �6$   �� 5G      $
��auto �6$   �� 5H      $
��auto �6$   �� 5I      $
��auto �6$   �� 5J      $
��auto �6$   �� 5K      $��* �6$   �� 5L      $
��auto �6$   �� 5M      $
��auto �6$   .+�5Q      � .+�5R      �� $
��Team��6    $��32(�6,   $��SemiBold"�6:   $
��Left=�5S      ���#12151867   5T      � 5V      $��2��6   .+�5X      �� -   5          
mailButton#��[ 5X   A   .��6e   @5Z      �� 5]      -   5          	addButton#��[ 5]   <   .��6_   Neues Teammitglied5_      5`      � 5b      $��3��6   $��0, 10, 0, 0q�6$   .+�5d      �� -   5        $ ��txtblock_teamember_counter��5d       $��18(�6B   $��SemiBold"�6P   ���#121518$
��Left=�5e   1   $��Center=�6L   5f      � 5h      $��4��6   $��0, 10, 0, 0q�6$   .+�5i      &   System.Windows.Controls.DataGrid  -   5        $��datagrid_teammembers��5i      
   AutoGenerateColumns$
 False��6;      Columns 5j      0  *System.Windows.Controls.DataGridTextColumn  5k      ,  &System.Windows.Controls.DataGridColumn   Header$ Hochschulkürzel��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6J   �� *
hs_kuerzel+  5l      $ Vorname��61    6B   �� *	vorname+  5m      $ Nachname��61    6C   �� *
nachname+  5n      $ Seminargruppe��61    6H   �� *seminargruppe+  5o      $ 	Abteilung��61    6D   �� *	abteilung+  5p      $ Bereich��61    6B   �� *	bereich+  5q      $ Position��61    6C   �� *
position+  5r      $ Beitrittsdatum��61    6I   �� *beitrittsdatum+  5s      $ Geburtsdatum��61    6G   �� *geburtsdatum+  5t      $ Handynummer��61    6F   �� *handynummer+5v      5w      �� 5{      $��75, 10q�6   $��6��6,   $	��0.8��69   ���#3333336F   �� 5}      $��7��6   $��8(�6)   ���#121518 �� TextAlignment$ Center=�6K   KITechnikum Mittweida Motorsport - University of Applied Sciences Mittweida�� 5~   `   1/Technikumplatz 17, 09648 Mittweida, Deutschland�� 5   F   0.info@tm-motorsport.net - www.tm-motorsport.net5�      5�      5�      5�      5�      