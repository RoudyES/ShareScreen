# ShareScreen
This is a .NET application for remote assistance (like TeamViewer and AnyDesk).

It is written in a modular way for easier integration in any .NET app. All you have to do is provide a UI and the rest is taken care of.
A sample UI app is also provided for reference on the required functions.

# DEMO

**Step 1:** Make sure the Broadcaster app is running (As admin), it could be made into a windows service or kept as console app. To make it a service just add "RunAsService" (Without quotes) in the "Conditional compilation symbols" in the build settings.<br/><br/>
**Step 2:** Change the client app's app.config (update the broadcaster ip value depending on where you ran the broadcaster).<br/><br/>
**Step 3:** Run "ShareScreen" app and make sure the status says connected to server.<br/><br/>
**Step 4:** To use as a client, write the host's ID and Password then click connect. To use as host just tell the client your credentials.<br/><br/>

An example of the application running is provided in the following image:
![alt text](ShareScreen/Demo.PNG?raw=true)
