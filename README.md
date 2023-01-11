# Tile Light and Shadows Like Terraria
 An example of how to draw Terraria style block lighting and shadows for Unity.

![Example](https://i.imgur.com/UVXPeVZ.png)


#How does it work?

1. There's 2 cameras.
2. Empty tiles are filled in with white blocks (only camera 2 render this)
3. A gaussian blur is applied to the camera rendering the white blocks
4. SpriteLightKit blends the two cameras, darkening everything not covered by the white blurry.
5. You can adjust the "light" penetration by changing the white tiles Pixels Per Unit. 

#Credit:

[prime31's SpriteLightKit](https://github.com/BigDaddyGameDev/SpriteLightKit)

[keijiro's GaussianBlur](https://github.com/keijiro/GaussianBlur)
