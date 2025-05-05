# Defold versus Unity

This repository is a combination of tests peformed in both the Unity engine and the Defold engine to compare the difference.

# Relevance
At the time or writing this, I am graduating at a company that uses both the Defold engine and the Unity engine. This is why I wanted to compare them on a couple of different metrics and see if there is a clear winner and/or what usecases both engines have.

# What aspects am I testing
I am going to be testing both engines on the following:
- Physics. Defold uses 2 different physics engines based on if 3D or 2D is selected. I want to compare them both to Unity's built-in physics
- General engine performance
- Buildsize
- A small part of usability assessment based on the prototypes made for the tests above

# How am I going to be testing these aspects
To give each engine a fair chance, I tested both engines "out of the box". This means that I did not do any optimizations on either side.

## Physics
For the physics test, the setup was almost the same for both 3D and 2D, minus a dimension of course:
- A box made of static objects
- A spawner that spawns an arbitrary amount of physics objects that fall right into the box

## General engine performance
For the general engine performance, I used the example shown in Smart penguins' comparison of Godot vs Unity game engine. This test involves moving sprites in a Unit circle:
- Spawning a prefab (a frying pan in my case) and letting it choose a random point in a Unit circle and moves there.
- A spawner spawning an arbitrary amount of these prefabs

This mostly tests the language and the engine's respective GameObjects API on how performant they are, because a lot of calculations are done in each prefab.

### Translating sprites
For translating objects, I didn't use any Unity nor Defold functions, because the primary functions did not line up. Unity uses transform.Translate(), while Defold doesn't use such a thing. In Defold, the normal way of moving an object is through animation. Defold allows you to animate the position of an object through go.Animate(), which also seems like the way to move objects that is being advertised the most. 

To give both engines an equal ground, I wrote the translation of objects myself, which you can see in [Defold](https://github.com/Mightylight/Advanced-tools/blob/main/Advanced_Tools_Defold/scripts/moving_pan.script) and in [Unity](https://github.com/Mightylight/Advanced-tools/blob/main/Advanced_Tools_Unity/Assets/Scripts/MovingPan.cs)

## Buildsize
This is something that came up while testing the physics and general performance of the engines. I noticed that the buildsizes were VERY different and decided to chart them aswell. The main idea is just to build on different platforms and chart them to see the difference.

# General information
## Settings changed
To make Defold handle these mass amounts of collisions and objects on the screen, I did have to tweak the game.project file to allow for such numbers. The following settings have been changed:
- collection.max_instances
- sprite.max_count
- physics.max_collision_object_count
- physics.max_collisions
- physics.max_contacts
- model.max_count

These settings are exclusive to Defold, as Unity was able to handle the amount of objects out of the box

## Resources and Assets used
I am going to be taking inspiration from Smart Penguins on Youtube, where he has made a series comparing the Godot and Unity on different aspects, as well as comparing Unity's C# and ECS, which you can view [here](https://www.youtube.com/playlist?list=PLrw9uFU6NhfO03AAFcY2JuFyAc-dSCHTd)

On the Defold side, I used a library to keep track of the FPS, called [defold-metrics](https://github.com/britzl/defold-metrics)

The pan sprite was something that I made myself before in a different project.

## Game engine versions
- Defold 1.9.2
- Unity 6 (6000.0.34f1)

## Specs
All tests below were conducted on a pc with the following specs:
- Geforce RTX 4070 12GB
- Ryzen 5 7600
- 32GB RAM


# Setup
## Physics tests
### Parameters
The parameters for the physics tests are the following:
- Objects per spawn cycle = 50
- Data sampling rate = every 50 objects
- Data size = 3000
- Spawning interval = 0.5

### Unity
![2d_physics_unity_showcase](https://github.com/user-attachments/assets/906f1645-b05f-4c72-bed4-8bc5c2b53acf)

This is the 2D setup

![3d_physics_unity_showcase](https://github.com/user-attachments/assets/84b0e4a0-7a6b-48fe-9a4a-78aa7182d5c6)

And this is what the 3D setup looks like.

### Defold
![2d_physics_defold_showcase](https://github.com/user-attachments/assets/afbc84f4-3ab0-40fb-9510-080ccf488786)

This is what the 2D setup looks like in Defold

![3d_physics_defold_showcase](https://github.com/user-attachments/assets/2bab086d-0f34-4f23-9a24-e66c5832a9c5)

And this is what the 3D setup looks like

#### Physics engines in Defold
Due to Defold using 2 different engines depending on wether you want to use 3D or 2D physics, it wasn't possible to include them both in 1 build, this is why you also see 2 seperate builds in related to defold as supposed to the 1 you see for Unity.

## General engine performance tests
### Parameters
The parameters for the general tests are the following:
- Objects per spawn cycle = 1000
- Data sampling rate = every 1000 objects
- Data size = 15000
- Spawning interval = 0.5

### Unity
![moving_sprites_unity_showcase](https://github.com/user-attachments/assets/9fa61ef6-84a8-4cc5-864a-fcd8b3c4046e)

This is the setup in Unity

### Defold

![moving_sprites_defold_showcase](https://github.com/user-attachments/assets/bb03a3f1-15e9-445a-816c-27cf0c3e1da0)

This is the setup in Defold

## Buildsize
For the buildsize, I took the project made for all the tests above and build them (release build) on Windows HTML5 and Android, since those are what I have at hand to see if they actually work. Since Defold uses 2 physics engines and I can't combine both the 2D and 3D physics tests AND have them working (which isn't neccesary for comparing buildsizes), I just made sure they were both in the build, which I can confirm by looking at the build report.

![build_report_scene_inclusion](https://github.com/user-attachments/assets/10a22c63-82e5-4c78-99e9-df854af34873)

All of the build reports can be found [here](https://github.com/Mightylight/Advanced-tools/tree/main/Build%20Reports)

Unity doesn't give an extensive build report like Defold has when building, but you can view it through the editor logs when the build is complete, which is where I got the data from and put it in a text file with every build.

# Test results
Below are the graph made from the raw data exported from both Unity and Defold. For the actual raw data, see [the spreadsheet](https://docs.google.com/spreadsheets/d/1YvHIkk_2cUsNn0eQcy1_qhapiYPnLo_APJyqvqw0ztY/edit?usp=sharing)
## Physics 2D

![2d_physics_defold-Vs-unity](https://github.com/user-attachments/assets/ec6c5eb8-bae9-4081-8c09-510eb1e5cb64)

## Physics 3D

![3d_physics_defold-Vs-unity](https://github.com/user-attachments/assets/851cf576-efe1-49cd-ac2a-c8400f61d2d4)

### Notes
As you can see from the 2 graphs above, Unity wins it in the long run. Defold starts at a higher FPS, but drops Considerably after the first couple of collisions happen. Especially the 3D physics in Defold seems really underwhelming as it reaches 60fps after only 1300 objects as where Unity never seems to even reach anywhere close, ending the test of 3000 objects at over 400 fps. With these lines and the amount of sampling points I ended up using, the Unity data is a bit more jagged. I am not entirely sure why that is, but it doesn't affect the outcome of this test since Unity seems to be clearly better in the physics department

## General engine performance

![moving_sprites_defold-Vs-unity](https://github.com/user-attachments/assets/34418cbd-de5c-4b85-80ca-ad04970e54fd)

### Notes
Here again, we can again see that Unity is just overall more performant wen it comes to the amount of sprites. Defold seems to be a bit close this time, but again like with the physics, the FPS plummets once the first sprites have been spawned. At the time of writing this, I am not sure what causes this, but I would imagine this can have something to with the languages. Since this test requires the most amount of code excecution in their respective languages (C# and Lua), this could be the factor. To rule this out, I could've wrote the same functionality in C++ in Defold and see if that changed anything, but I feel like that's out of the scope of comparing Defold to Unity.

To get a more clear graph, I removed the 0 values from this graph, otherwise the graph would be unreadable on the far right. The raw data mentioned before still contains this data.

## Buildsize

![build_size_defold-Vs-unity](https://github.com/user-attachments/assets/dc4131e9-2ae7-4b6d-8456-42c10e58424a)

### Notes
Just like the other tests, there's a clear winner, but this time its Defold. Defolds buildsize is extremely small compared to Unity. I could see this being the redeeming factor that some games would be made with Defold. For both engines the filesize could obviously be reduced through optimizations, but I feel with this enormous gap there is no way you can get Unity to Defolds level. I think this is especially great if you are making the game in a HTML5 context, given that Defold also doesn't seem to use WebGL (this could change with the new WebGPU feature that is up and coming in Unity)

# Usability Comparison

When using both engines in this project, it became apparent that Unity is way more user friendly.

## Components
The biggest example has got to be the component system. In Unity everything is generalized to a GameObject, which also holds UI. In Defold, you have to make seperate components for those, called .go files and .gui files respectively. If you then want to have a button on the UI to interact with the spawner for example, you need to specifically make a .gui_script which has access to the gui API.

## Buildspeed
This is tied to the buildsize, but it is worth noting for if you are testing something in a mobile/HTML5 build (specifically something you can't test in editor). Defold's build speeds are considerably faster and you also don't need to switch platforms before building, which you need to do in Unity. This allows you to test and iterate way faster if you are debugging something, which I really enjoy.

## 3D
For these tests, I included 3D, because I knew Defold supported it. However after having worked with it, it is definitely in its early stages. My biggest frustration was building a 3D scene, while only have access to a 2D camera. This requires me to build the game everything I wanted to check something. Unity is definitely more build to handle 3D scenes and thus I would not reccomend making 3D games in Defold for now.

## Prototype speed
While not supported by data, I did find that I spent about equal time constructing prototypes in both engines. I might have been a bit faster in Unity, but that's due to me already working with Unity for over 4 years, while only having picked up Defold this year. I think the language also helped a bit with this. Lua's lack of static types allowed me to really easily modify variables. In the case for collecting data, the tables really came in handy. It is a bit of a learning curve, but in some scenario's I think the tables came in clutch with making these prototypes.

## Built-in script editor
Defold has a built-in script editor in its editor. This allows for quick variable changes and/or code changes, without me requiring to start up another program. I do definitely reccomend using visual studio code for general use with this engine, but it is nice that I can just change some stuff without needing visual studio on a second screen.

# Conclusion
At first, I wanted to immediately write off Defold against a giant like Unity, but I've come to realise that Defold definitely has its usecases. The small buildsizes and faster prototype speed on multiple playforms, are definitely aspects that I could see being beneficial, especially when you stick to GUI components to make small games, I think Defold has its uses.

For general performance, I would still choose Unity over Defold, but in specific cases, I would absolutely try out Defold.
