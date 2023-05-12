# Xabbo Packet Recorder
This is a Habbo packet recording script that captures and saves data packets sent and received. The saved packets can be replayed as a recording.

[![Xabbo Recording](https://github-production-user-asset-6210df.s3.amazonaws.com/34200697/237917268-a2780203-d6e9-4482-915d-9061a415ccc9.png)](https://www.youtube.com/watch?v=SBLT-rNKJpU)

### Xabbo Scripter
It is a C# scripting interface for G-Earth. You will need it to run the packet recorder. Click [here] to download Xabbo.

### How it Works
The script intercepts the data that is being sent and received by the Habbo client and saves it in a text file. Each packet contains information about a specific action, such as moving a character or saying something in the chat. By capturing and saving these packets, the script provides a complete record of everything that happened during the recording session.

Please note that the script has some limitations to keep in mind. Habbo updates it's SWF from time to time, and sometimes they change packet headers and/or structures. If the header of a recorded packet is not found, it will not be sent to the client. If the header is found but the structure changed, unexpected things may happen.

The script sends only Client Side packets, so even if the packet structures changes, it shouldn't be a problem in most cases. **I take no responsibility for the consequences of using this scripts** though, use it at your own risk.

Additionally, if a room is closed or deleted, if you're banned from the room or cannot access it for any other reasons, the recording may not work as expected, since access to the room is required to play the packets.

### How to Use
To use the Xabbo packet recorder:

- Copy paste both "xabbo-recorder.csx" and "xabbo-player.csx" to your Xabbo Scripter.
- Before running the recording script, go to the hotel view. It's important for you to load the room only after running the script, so you can load the users, furni and other important data.
- Run the recording script while playing Habbo. If you've recorded other script on the same path before, it will be overwrited.
- The script will automatically capture and save packets as you play.
- To replay the saved packets, just run the play script.
- You can edit the script path by changing "record_1.txt" on both scripts. This way, you can save multiple recordings.

### Notes
- The code was created on Flash version. In theory, you can change the ClientType to Unity in the scripts, but I didn't test it.
- You will need to have access to the same rooms that were recorded in order to properly replay the packets.
- While playing the records, almost all CS packets are blocked. You'll still receive friend requests and console messages though.
- The script won't record most of the UI interactions, since they are not handled by packets.

### Contributing
Contributions are welcome! If you find a bug or have an idea for a new feature, please create a pull request or open an issue on GitHub.

   [here]: <https://github.com/b7c/Xabbo.Scripter>