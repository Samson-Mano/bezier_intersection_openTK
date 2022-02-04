# bezier_intersection_openTK

This project provides visualization of distance between two bezier curves. The original objective was to explore novel methods to find the intersection points of two bezier curves. However trying to visualize heat map only using GDI+ graphics turns out to be extremely inefficient. So I shifted to implement modern openGL graphics to display nxn grid of quadrilaterals (two triangles) along with the contour lines. This is my first project using openGL and I have lot to learn.

![Bezier Heatmap](/Images/bezier_heatmap.gif)

![Bezier Intersection Explaintation](/Images/bezier_expl.png)

![Bezier Intersection Features](/Images/bezier_features.png)
