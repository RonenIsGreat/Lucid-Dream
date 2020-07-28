using BdtCasMessage;
using NavMessage;
using System.Windows;
using System.Threading;
using BeamBusFasTas;
using IDRS;
using PrsStaveBus;
using System.Windows.Media;

namespace MainSendWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NavRabbitMQ _navRabbitMQ;
        private BdtCasRabbitMQ _bdtCasRabbitMQ;

        #region Threads

        Thread beamBusCasSenderThread = new Thread(delegate ()
        {
            BeamBusCas.BeamBusCasSender.SendMessage();
        });

        Thread beamBusFasTasSenderThread = new Thread(delegate ()
        {
            BeamBusFasTasSender.SendMessage();
        });

        Thread IDRSSenderThread = new Thread(delegate ()
        {
            IdrsSender.SendMessage();
        });

        Thread PrsSenderThread = new Thread(delegate ()
        {
            PrsStaveBusSender.SendMessage();
        });


        #endregion

        #region Once threads
        Thread beamBusCasSenderOnceThread = new Thread(delegate ()
        {
            BeamBusCas.BeamBusCasSender.SendMessageOnce();
        });

        Thread beamBusFasTasSenderOnceThread = new Thread(delegate ()
        {
            BeamBusFasTasSender.SendMessageOnce();
        });

        Thread IDRSSenderOnceThread = new Thread(delegate ()
        {
            IdrsSender.SendMessageOnce();
        });

        Thread PrsSenderOnceThread = new Thread(delegate ()
        {
            PrsStaveBusSender.SendMessageOnce();
        });
        #endregion


        private void BeamBusCasSendMessage()
        {
            beamBusCasSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            beamBusCasSenderThread.Start();
        }

        private void BeamBusFasTasSendMessage()
        {
            beamBusFasTasSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            beamBusFasTasSenderThread.Start(); 
        }

        private void IdrsSendMessage()
        {
            IDRSSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            IDRSSenderThread.Start();
        }

        private void PrsSendMessage()
        {
            PrsSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            PrsSenderThread.Start();
        }

        private void restartAllThreads()
        {
            beamBusCasSenderThread.Abort();
            beamBusCasSenderThread = null;
            beamBusCasSenderThread = new Thread(delegate ()
            {
                BeamBusCas.BeamBusCasSender.SendMessage();
            });


            beamBusFasTasSenderThread.Abort();
            beamBusFasTasSenderThread = null;
            beamBusFasTasSenderThread = new Thread(delegate ()
            {
                BeamBusFasTasSender.SendMessage();
            });

            IDRSSenderThread.Abort();
            IDRSSenderThread = null;
            IDRSSenderThread = new Thread(delegate ()
            {
                IdrsSender.SendMessage();
            });

            PrsSenderThread.Abort();
            PrsSenderThread = null;
            PrsSenderThread = new Thread(delegate ()
            {
                PrsStaveBusSender.SendMessage();
            });

            //Once threads

            beamBusCasSenderOnceThread.Abort();
            beamBusCasSenderOnceThread = null;
            beamBusCasSenderOnceThread = new Thread(delegate ()
            {
                BeamBusCas.BeamBusCasSender.SendMessageOnce();
            });


            beamBusFasTasSenderOnceThread.Abort();
            beamBusFasTasSenderOnceThread = null;
            beamBusFasTasSenderOnceThread = new Thread(delegate ()
            {
                BeamBusFasTasSender.SendMessageOnce();
            });

            IDRSSenderOnceThread.Abort();
            IDRSSenderOnceThread = null;
            IDRSSenderOnceThread = new Thread(delegate ()
            {
                IdrsSender.SendMessageOnce();
            });

            PrsSenderOnceThread.Abort();
            PrsSenderOnceThread = null;
            PrsSenderOnceThread = new Thread(delegate ()
            {
                PrsStaveBusSender.SendMessageOnce();
            });
        }

        private void BeamBusCasSendMessageOnce()
        {
            beamBusCasSenderOnceThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            beamBusCasSenderOnceThread.Start();
        }

        private void BeamBusFasTasSendMessageOnce()
        {
            beamBusFasTasSenderOnceThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            beamBusFasTasSenderOnceThread.Start();
        }

        private void IdrsSendMessageOnce()
        {
            IDRSSenderOnceThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            IDRSSenderOnceThread.Start();
        }

        private void PrsSendMessageOnce()
        {
            PrsSenderOnceThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            PrsSenderOnceThread.Start();
        }
        public MainWindow()
        {
            InitializeComponent();
            //_navRabbitMQ = new NavRabbitMQ(Settings.Default.Nav_ExchangeName);
            //_bdtCasRabbitMQ = new BdtCasRabbitMQ(Settings.Default.BdtCas_ExchangeName);
        }

        private void sendMessages_Click(object sender, RoutedEventArgs e)
        {
            if(SendButton.Content.ToString() == "Start sending")
            {
                SendButton.Content = "Stop sending";
                SendButton.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

                SendOnce.Content = "Stop sending";
                SendOnce.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

                if (BeamBus_Cas.IsChecked.Value )
                {
                    BeamBusCasSendMessage();
                }

                if (BeamBus_FasTas.IsChecked.Value)
                {
                    BeamBusFasTasSendMessage();
                }

                if(IDRS.IsChecked.Value)
                {
                    IdrsSendMessage();
                }

                if(StaveBus_Prs.IsChecked.Value)
                {
                    PrsSendMessage();
                }
            }
            else
            {
                SendButton.Content = "Start sending";
                SendButton.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));

                SendOnce.Content = "Send Once";
                SendOnce.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));

                restartAllThreads();
            }

        }

        private void sendMessagesOnce_Click(object sender, RoutedEventArgs e)
        {
            if (SendOnce.Content.ToString() == "Send Once")
            {
                SendOnce.Content = "Stop sending";
                SendOnce.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

                SendButton.Content = "Stop sending";
                SendButton.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

                if (BeamBus_Cas.IsChecked.Value)
                {
                    BeamBusCasSendMessageOnce();
                    
                }

                if (BeamBus_FasTas.IsChecked.Value)
                {
                    BeamBusFasTasSendMessageOnce();
                }

                if (IDRS.IsChecked.Value)
                {
                    IdrsSendMessageOnce();
                }

                if (StaveBus_Prs.IsChecked.Value)
                {
                    PrsSendMessageOnce();
                }
            }
            else
            {
                SendOnce.Content = "Send Once";
                SendOnce.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));

                SendButton.Content = "Start sending";
                SendButton.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));

                restartAllThreads();
            }
        }

        #region Buttons Events
        private void sendBeamBusCasMessages_Click(object sender, RoutedEventArgs e)
        {

            Thread beamBusCasSenderThread = new Thread(delegate ()
            {
                BeamBusCas.BeamBusCasSender.SendMessage();
            });

            beamBusCasSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            beamBusCasSenderThread.Start();
        }

        private void sendBeamBusFasTasMessages_Click(object sender, RoutedEventArgs e)
        {

            Thread beamBusFasTasSenderThread = new Thread(delegate ()
            {
                BeamBusFasTasSender.SendMessage();
            });

            beamBusFasTasSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            beamBusFasTasSenderThread.Start();
        }

        private void sendIDRSMessages_Click(object sender, RoutedEventArgs e)
        {

            Thread IDRSSenderThread = new Thread(delegate ()
            {
                IdrsSender.SendMessage();
            });

            IDRSSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            IDRSSenderThread.Start();
        }

        private void sendStaveBusPrsMessages_Click(object sender, RoutedEventArgs e)
        {

            Thread PrsSenderThread = new Thread(delegate ()
            {
                PrsStaveBusSender.SendMessage();
            });

            PrsSenderThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            PrsSenderThread.Start();
        }

        private void sendNavMessages_Click(object sender, RoutedEventArgs e)
        {
            
            //OriginalNavMessage navObject = new OriginalNavMessage();

            //while (true)
            //{
            //    _navRabbitMQ.SendMessage(navObject);
            //    Thread.Sleep(1000);
            //}

        }

        private void sendAllMessages_Click(object sender, RoutedEventArgs e)
        {
            //OriginalBdtCasMessage bdtCasObject = new OriginalBdtCasMessage();
            //OriginalNavMessage navObject = new OriginalNavMessage();
            //while (true)
            //{
            //    _bdtCasRabbitMQ.SendMessage(bdtCasObject);
            //    _navRabbitMQ.SendMessage(navObject);
            //    Thread.Sleep(1000);
            //}

        }

        private void sendBdtCasMessages_Click(object sender, RoutedEventArgs e)
        {
            //OriginalBdtCasMessage bdtCasObject = new OriginalBdtCasMessage();
            //while (true)
            //{
            //    _bdtCasRabbitMQ.SendMessage(bdtCasObject);
            //    Thread.Sleep(1000);
            //}
        }

        private void enableButtons()
        {

        }
        #endregion

        
    }
}
