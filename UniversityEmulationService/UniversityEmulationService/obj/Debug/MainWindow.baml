   M S B A M L   `   `   `  ����U  QUniversityEmulationService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:UniversityEmulationServiceUniversityEmulationService  5   	   +    %UniversityEmulationService.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:UniversityEmulationService     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��725��6)   .��5	      � .+�5
   
   �� $��Emulation service��6   $��5q�6.   $��3q�6:   .��5      ]� .+�5      �� $��40��6   $	��Top=�6)    �� Orientation$ 
Horizontal=�6>   .+�5      �� -   5        $��Universities��5      $
��20,4q�63   $	��160��6B   $��7q�6N   $ �� IsSynchronizedWithCurrentItem$
 TrueY�6�   ��6Y   ��  �� Path$ BindUniversityVariant� �� SelectedValue 6�   �� *BindUniversitySelected+6�   �� 5      -   5        $��
StartYears��5      $��3,0,3,0q�60   $��4q�6A   $��Center=�6M    �� TextAlignment$ Center=�6h   $��50��6�   ��6   �� *BindStartYear+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5      -   5        $��EndYears��5      $��3,0,3,0q�6.   $��4q�6?   $��Center=�6K   $ Center=�6f   $��50��6�   ��6}   �� *BindEndYear+$ TwoWay=�$ PropertyChanged=�6�   �� 5      $
��20,0q�6   $��7q�6,   $��Start��67   ��6G   �� *actionStart+�� 5      $
��20,0q�6   $��7q�6,   $
��Stop��67   ��6F   �� *
actionStop+5      �� 5      $	��Top=�6   $ Vertical=�62   .+�5      �� $ 
Horizontal=�6!   .+�5      ��  �� Padding$
 10,5q�6$   $*��$Current date in university timezone:��63   �� 5      $ 5q�6$   ��60   �� *BindCurrentDate+�� 5      $ 5q�6$   $&�� Current date in local time zone:��60   �� 5      $ 5q�6$   ��60   �� *BindCurrentDateSTZ+5      �� 5      $ 
Horizontal=�6!   .+�5      �� $
 10,5q�6$   $*��$Number of students added this month:��63   �� 5      $	 0,5q�6$   ��62   �� *BindNumberAddedStudents+5      �� 5      $ 
Horizontal=�6!   .+�5       �� $
 10,5q�6$   $+��%Number of students expelt this month:��63   �� 5!      $	 0,5q�6$   ��62   �� *BindNumberExpeltStudents+5"      �� 5#      $ 
Horizontal=�6!   .+�5$      �� $
 10,5q�6$   $)��#The largest number of the incoming:��63   �� 5%      $	 0,5q�6$   ��62   �� *BindLargestNumberOfIncoming+5&      5'      ]� 5(      $��Bottom=�6   $��30��64   $��Bottom=�6@   .+�5)      �� $��Right=�6    $
 10,0q�67   ���#667$��Center=�6X   	 �� TextAlignment$	 Right=�6s   ��6�   �� *ServerStatus+� 5*      $
��Left=�6"   $
��Left=�68   $��5q�6S   $	��400��6^   
 � Value
 6j   �� *BindProgress+$ PropertyChanged=�$ OneWay=�5+      5,      5-      5.      5/      