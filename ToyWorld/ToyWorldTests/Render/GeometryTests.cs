﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Game;
using GoodAI.ToyWorld.Control;
using OpenTK.Input;
using Render.RenderRequests;
using Render.RenderRequests.Tests;
using Xunit;

namespace ToyWorldTests.Render
{
    public class GeometryTests
    {
        [Fact]
        public void BuildGeometries()
        {

        }

        [Fact(Skip = "Manual input needed")]
        //[Fact]
        public void DrawBasicGeometry()
        {
            var gc = ControllerFactory.GetController() as GameControllerBase;
            gc.Init(null);

            Key winKeypressResult = default(Key);
            gc.Renderer.Window.KeyDown += (sender, args) => winKeypressResult = args.Key;
            gc.Renderer.Window.Visible = true;


            var RRTest = gc.RegisterAvatarRenderRequest<IAvatarRenderRequestFoV>(0);
            Assert.NotEmpty(RRTest.Image);


            while (winKeypressResult == default(Key))
            {
                gc.MakeStep();
                gc.Renderer.Context.MakeCurrent(gc.Renderer.Window.WindowInfo);
                gc.Renderer.Context.SwapBuffers();
                Thread.Sleep(100);
            }


            Assert.Contains(RRTest.Image, u => u != 0xFFFFFF00 || u != 0);

            gc.Dispose();


            Assert.Equal(winKeypressResult, Key.A);
        }
    }
}
