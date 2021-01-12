using Microsoft.Xna.Framework;
using Nez;
using Nez.BitmapFonts;
using Nez.Sprites;
using Nez.UI;
using System;
using System.Collections.Generic;

namespace codex_online
{

    public class CardListWindow : Entity
    {
        private readonly int cardsOnScreen = 20;
        private readonly float distanceBetweenCards;
        private readonly string hide = "Hide";
        private readonly string done = "Done";
        private readonly string show = "Return";
        private readonly string selectMinimumText = "Select at least {0} more cards";
        private readonly string selectMaximumText = "Select up to {0} more cards";
        private readonly ClientState clientState;
        private readonly List<CardUi> selectedCards = new List<CardUi>();
        private readonly BitmapFont font;

        private UICanvas window; 
        private List<CardUi> cards;
        private Table table;
        private int minimumSelection;
        private int maximumSelection;
        private Slider slider;
        private Cell sliderCell;
        private TextButton hideButton;
        private Cell hideButtonCell;
        private TextButton showButton;
        private Entity showButtonEntity;
        private Label selectText;

        public TextButtonStyle TextButtonStyle { get; set; }

        public CardListWindow(ClientState clientState)
        {
            this.clientState = clientState;
            distanceBetweenCards = CardUi.CardWidth / 2;
            Name = "Card List Window";
            TextButtonStyle = TextButtonStyle.Create(Color.SandyBrown, Color.SandyBrown, Color.SaddleBrown);

            Position = new Vector2(GameClient.ScreenWidth / 2, GameClient.ScreenHeight / 2);
            window = new UICanvas();
            table = window.Stage.AddElement(new Table()).SetFillParent(true);
            AddComponent(window);

            slider = new Slider(0, 100, .01f, false, SliderStyle.Create(Color.DarkGray, Color.LightYellow))
            {
                //TODO: make this number more meaningful
                SliderBoundaryThreshold = 5000f
            };

            selectText = new Label(String.Format(selectMinimumText, 0));
            selectText.SetFontScale(3);

            table.Align(Convert.ToInt32(Align.Top));
            table.Add();
            table.Add(selectText);
            table.Row();

            table.Add().SetMinHeight(GameClient.ScreenHeight / 2 + CardUi.CardHeight / 2 - selectText.MinHeight);
            table.Row();

            sliderCell = table.Add(slider).SetMinWidth(distanceBetweenCards * cardsOnScreen).SetColspan(3);
            table.Row();

            TextButton doneButton = new TextButton(done, TextButtonStyle);
            doneButton.OnClicked += button =>
            {
                if (selectedCards.Count >= minimumSelection)
                {
                    CloseWindow();
                }
            };
            table.Add(doneButton).SetMinWidth(CardUi.CardWidth).SetMinHeight(CardUi.CardWidth / 2);
            table.Add();

            hideButton = new TextButton(hide, TextButtonStyle);
            hideButton.OnClicked += button =>
            {
                showButtonEntity.Enabled = true;
                Disable();
            };
            hideButtonCell = table.Add(hideButton).SetMinWidth(CardUi.CardWidth).SetMinHeight(CardUi.CardWidth / 2);

            showButton = new TextButton(show, TextButtonStyle);
            showButton.OnClicked += button =>
            {
                Enable();
                showButtonEntity.Enabled = false;
                UpdateCardPositions();
                UpdateSelectedCardPositions();
            };
            showButton.SetWidth(CardUi.CardWidth);
            showButton.SetHeight(CardUi.CardWidth / 2);

            UICanvas showCanvas = new UICanvas();
            showButtonEntity = new Entity();
            showCanvas.Stage.AddElement(showButton).SetPosition(GameClient.ScreenWidth - CardUi.CardWidth, GameClient.ScreenHeight - CardUi.CardWidth / 2);
            showButtonEntity.AddComponent(showCanvas);
            showButtonEntity.Enabled = false;
            
            Enabled = false;
        }

        public void OpenWindow(List<CardUi> cards, bool selecting, int minimumSelection = 0, int maximumSelection = 0)
        {
            if (!Scene.Entities.Contains(showButtonEntity))
            {
                Scene.AddEntity(showButtonEntity);
            }
            clientState.State = ClientState.CardListWindow;
            this.minimumSelection = minimumSelection;
            this.maximumSelection = maximumSelection;

            if (selecting)
            {
                hideButtonCell.SetElement(hideButton);
            }
            else
            {
                hideButtonCell.SetElement(null);
            }

            this.cards = cards;
            int totalCards = cardsOnScreen < cards.Count ? cardsOnScreen : cards.Count;
            for (int x = 0; x < this.cards.Count && x < cardsOnScreen; x++)
            {
                CardUi card = this.cards[x];
                card.Parent = window.Transform;
                DrawCard(card, x, 0, totalCards, 0, distanceBetweenCards);
            }
            
            if (cards.Count > cardsOnScreen)
            {
                sliderCell.SetElement(slider);
            }
            else
            {
                sliderCell.SetElement(null);
            }

            if (minimumSelection > 0 || maximumSelection == 0)
            {
                selectText.SetText(String.Format(selectMinimumText, minimumSelection));
            }
            else
            {
                selectText.SetText(String.Format(selectMaximumText, maximumSelection));
            }

            Enabled = true;
        }

        public void CloseWindow()
        {
            Disable();
            selectedCards.Clear();
            cards.Clear();
            clientState.State = ClientState.InGame;
        }

        public override void Update()
        {
            base.Update();
            if (Enabled)
            {
                UpdateCardPositions();
            }
        }

        public void SelectCard(CardUi card)
        {
            if (cards.Contains(card))
            {
                if (maximumSelection == 0 || selectedCards.Count < maximumSelection)
                {
                    cards.Remove(card);
                    selectedCards.Add(card);
                }
            }
            else if (selectedCards.Contains(card))
            {
                selectedCards.Remove(card);
                cards.Add(card);
            }
            UpdateSelectedCardPositions();
            UpdateCardPositions();


            if (selectedCards.Count < minimumSelection || maximumSelection == 0)
            {
                int cardsLeftToSelect = minimumSelection - selectedCards.Count < 0 ? 0 : minimumSelection - selectedCards.Count;
                selectText.SetText(String.Format(selectMinimumText, cardsLeftToSelect));
            }
            else
            {
                selectText.SetText(String.Format(selectMaximumText, maximumSelection - selectedCards.Count));
            }
        }

        private void UpdateCardPositions()
        {
            cards.ForEach(card => card.Enabled = false);
            

            int shownCardRangeStart;
            float ratioMovedOverFirstCard;
            if (cards.Count > cardsOnScreen)
            {
                float sliderIndex = slider.GetVisualPercent() * (cards.Count - cardsOnScreen);
                shownCardRangeStart = Convert.ToInt32(Math.Floor(sliderIndex));
                ratioMovedOverFirstCard = sliderIndex % 1;
                sliderCell.SetElement(slider);
            }
            else
            {
                sliderCell.SetElement(null);
                shownCardRangeStart = 0;
                ratioMovedOverFirstCard = 0;
            }

            int totalCards = cardsOnScreen < cards.Count ? cardsOnScreen : cards.Count;

            if (cards.Count > 0)
            {
                DrawCard(cards[shownCardRangeStart], 0, 0, totalCards, 0, distanceBetweenCards);
            }

            for (int x = 1; shownCardRangeStart + x < cards.Count && x < cardsOnScreen; x++)
            {
                DrawCard(
                    cards[shownCardRangeStart + x],
                    x,
                    ratioMovedOverFirstCard * distanceBetweenCards,
                    totalCards,
                    0,
                    distanceBetweenCards
                );
            }

            if (cards.Count - shownCardRangeStart > cardsOnScreen && ratioMovedOverFirstCard > 0)
            {
                int lastCardIndex = shownCardRangeStart + cardsOnScreen;
                DrawCard(
                    cards[lastCardIndex],
                    cardsOnScreen - 1,
                    0,
                    totalCards,
                    0,
                    distanceBetweenCards
                );
                cards[lastCardIndex].GetComponent<SpriteRenderer>().LayerDepth = 1 + LayerConstant.LayerDepthIncriment;
            }
        }

        private void UpdateSelectedCardPositions()
        {
            float distanceBetweenCards = selectedCards.Count >= GameClient.ScreenWidth / CardUi.CardWidth ?
                (GameClient.ScreenWidth - CardUi.CardWidth) / (selectedCards.Count - 1) :
                CardUi.CardWidth;
            for (int x = 0; x < selectedCards.Count; x++)
            {
                DrawCard(selectedCards[x], x, 0, selectedCards.Count, -CardUi.CardHeight, distanceBetweenCards);
            }
        }

        private void DrawCard(CardUi card, int index, float horizontalAdjustment, int totalCards, float yPosition, float distanceBetweenCards)
        {
            card.Parent = Transform;
            card.LocalPosition = new Vector2(
                distanceBetweenCards * (index - (totalCards - 1) / 2f) - horizontalAdjustment, 
                yPosition
            );
            card.GetComponent<SpriteRenderer>().RenderLayer = LayerConstant.CardListWindowRenderLayer;
            card.GetComponent<SpriteRenderer>().LayerDepth = 1 - index * LayerConstant.LayerDepthIncriment;
            Flags.SetFlag(ref card.GetComponent<BoxCollider>().PhysicsLayer, Convert.ToInt32(PhysicsLayerFlag.WindowOpen));
            card.Enabled = true;
        }
        private void Disable()
        {
            void disableCards(CardUi card)
            {
                Flags.SetFlagExclusive(ref card.GetComponent<BoxCollider>().PhysicsLayer, Convert.ToInt32(PhysicsLayerFlag.Default));
                card.Parent = null;
                card.Enabled = false;
            }
            cards.ForEach(disableCards);
            selectedCards.ForEach(disableCards);
            Enabled = false;
        }

        private void Enable()
        {
            void enableCards(CardUi card)
            {
                Flags.SetFlagExclusive(ref card.GetComponent<BoxCollider>().PhysicsLayer, Convert.ToInt32(PhysicsLayerFlag.Default)); card.Parent = null;
                card.Enabled = true;
            }
            cards.ForEach(enableCards);
            selectedCards.ForEach(enableCards);
            Enabled = true;
        }
    }
}
