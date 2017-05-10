# KinderMediaPlayer

```diff
- NOTE: KinderMediaPlayer is still early in development and not yet ready for use
```

KinderMediaPlayer is a simple Kiosk-like application that allows children to view and listen to predefined media content (Videos, Images and Audio).

### Features

 - Easy to use interface for children with an emphasize on visual navigation instead of relying on text
 - Easy to use content selection for the administrator
 - Disable keyboard input (except ctrl-alt-del)

Currently KinderMediaPlayer is only available on Windows.

### How to use

The Administrator can configure and exit the application by accessing the **Admin**-Panel via the bottom right button. The default password is "admin" and should be changed.

In the **Admin**-Panel, content can be selected for the children and a the UI can be altered.

### FAQ

**How do I reset the password?**

First, you should exit the application via ctrl-alt-del and terminate the process manually. Then, locate the installation folder and open the "settings.xml" file. Delete the line that says

```xml
  <PASSWORD> xxx </PASSWORD>
```

and restart the application. The password should now be reset to "admin".

**How do I close the app?**

This is done via the **Admin**-Panel. If you have forgotten the password and want to close the app, see "*How do I reset the password*".
