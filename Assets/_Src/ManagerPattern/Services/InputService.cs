using System;
using UniRx;
using UnityEngine;

namespace Game.Services
{
    public class InputService 
    {
        private readonly Subject<Vector2> onMove = new Subject<Vector2>();

        public IObservable<Vector2> OnMove => onMove;

        public InputService()
        {
            //var input = new PlayerInputActions();
            //input.Player.Move.performed += ctx => onMove.OnNext(ctx.ReadValue<Vector2>());
            //input.Player.Enable();
        }
    }
}

