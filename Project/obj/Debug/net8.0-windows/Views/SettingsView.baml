   M S B A M L   `   `   `  ����B  >Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Project.ViewsProject.Views  5           Project.Views.SettingsView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -localclr-namespace:Project.Views     5      $	��800��5      $	��950��6     � 	Resources  5         ComboBoxToggleButton �� 
TargetType �� Padding F�Name �� 
TargetType �� Property �� Value � ColumnDefinitions �� CornerRadius	 ��Name
 �� 
IsReadOnly �� 	ClickMode d� 	IsChecked �� Mode �� RelativeSource �Name �� IsDropDownOpen � AllowsTransparency � ActualWidth J� SnapsToDevicePixels K� IsItemsHost-  'System.Windows.Input.KeyboardNavigation   DirectionalNavigation��%�  &	        '�� �     7   �� 5	   
   " d�67   .R�5
      �� 
������ 
������ $��0q�5      $ 10q�5      ._�5      F� $ Arrow��6   ���#4CAF50$��Center=�5      $��Center=�5      "����        � 	=  	= � z     	5      5      5      �� 5   
   " ��6   .�5      ��  � ��$ 16(�6)   �� 5       � ��$ 10q�6(   �� 5       � ��
 ���� 6+   �� 5       � �� �#2E7D326+   �� 5       � �� �#4CAF506,   �� 5       � ��$ 2q�60   �� 5       � ��$ 40��6'   �� 5        � ��$ 0,0,0,15q�6'   �� 5!       �@��
IsEditable$
 True��6+   �� 5"       � �� 5#      �� 5$      " ��6&   .R�5%      �  5&      �� 5'   "   �� 5(   "   $
��Auto �63   .+�5,      �� #��z��6%   #��z��5-   !   #��z��5.   !   $ 12��5/   !   $��2��50   !   �� 53      $	 PART_EditableTextBox��6&   
������ $��0q�56   "   #��z��57   "   #��z��58   "   $��8,0,30,0q�59   "   $

 True��5:   "   $��Center=�5;   "   $��0��5<   "   ��54   "   Z� 5<   "   d� 5?      $��1��6+   8��  $|�False��5B   '   $ Press=�5C   '   $��30��5D   '   $��30��5E   '   $��0,0,8,0q�5F   '   
������  5A   '   �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+5G   '   � 5J      $ 
PART_Popup��6$   $��Bottom=�5K       #��z 5L       $
 True��5M       $|�False��5N       $
��Fade=�5O       .��5P   "   � $	��200��6'   #��z 5Q   #   
�������$
 True��5S   #   .+�5T   &   �� 
�������$ 6��6@   ._�5U   *   �� .��5V   .   �� $
 True��69   $ 	Contained=�5W   5   5X   +   5Y   '   5Z   #   5[      5\      5]      5_      5`      5c      .��5e      �   AppBackgroundBrush#���  6   .+�5h   
   �� -   5          BackToMainMenu#���  5h        RoundedButton#��[ 6<   $
��Left=�5i      $	��Top=�6,   $��	10,35,0,0q�5j       J� RenderTransformOrigin$ 	0.5,0.558&�5k      �� 5m   
     Select Theme#���  6   $��24(�5n      $
��Bold"�5o      
���"�"�$��Center=�5q      $��0,412,0,-392q�5r      �� 5t   
   -   5          Switch Theme#���  5t      $	��168��5u   	   $��49��6   $��16(�5w   	   $��0q�5x   	   $��Center=�5y   	   $	��Top=�5z   	   ���	#804CAF50
�������$��	0,475,0,0q�5|      ��5}      �� 5~      " ��6"   .R�5      ��  ��Name$ border��6   #��z��6-   $ 20��5�      #��z��5�      #��z��5�       J� Effect 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�         
BlurRadius$ 15Q�6/      ShadowDepth$ 3Q�6?      	Direction$	 320Q�6O      Color$ Black��6_      Opacity$	 0.5Q�6m   ._�5�      �� $��Center=�6+   $��Center=�5�   #   $
��True��5�   #   5�       �� Triggers 5�      P� 5�       P� Property �@J�IsMouseOver  P� Value$
  True��69   ! N� EnterActions! 5�      �� 5�   "   .��5�   &   �� .��5�   *   W� " �� 
TargetName$" border��5�   %   # �� TargetProperty$9# 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�5�   %   	$ W� To$ 1.15�   %   % g� Duration$% 0:0:0.2D�6.   W� 5�   *   $" border��5�   %   $9# 3(UIElement.RenderTransform).(ScaleTransform.ScaleY)�5�   %   $ 1.15�   %   $% 0:0:0.2D�6.   �� 5�   *   $" border��5�   %   $1# +(Border.Background).(SolidColorBrush.Color)�5�   %   	& �� To& 	#B345A0495�   %   $% 0:0:0.2D�64   5�   '   5�   #   ' N� ExitActions' 5�      �� 5�   "   .��5�   &   �� .��5�   *   W� $" border��5�   %   $9# 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�5�   %   $ 1.05�   %   $% 0:0:0.2D�6.   W� 5�   *   $" border��5�   %   $9# 3(UIElement.RenderTransform).(ScaleTransform.ScaleY)�5�   %   $ 1.05�   %   $% 0:0:0.2D�6.   �� 5�   *   $" border��5�   %   $1# +(Border.Background).(SolidColorBrush.Color)�5�   %   & 	#804CAF505�   %   $% 0:0:0.2D�64   5�   '   5�   #   5�      5�      5�      �� 5�   
   $��Center=�6   $	��Top=�62   $��	0,300,0,0q�6J   ( �� Orientation$( Vertical=�6]   .+�5�      ��   Select Language#���  6   $��24(�5�      $
��Bold"�5�      
���"�"�$��Center=�5�      $��0,0,0,20q�5�      �� 5�      -   5        ) ��Name$) LanguageComboBox��5�      $	��260��5�      .@�5�      �� $��English��6   
* � Tag$* en��61   �� 5�      $��Polski��6   $* pl��60   5�      5�      5�      5�      