﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MacroBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<WorkflowView> FavWorkflows = new();
        private List<WorkflowView> Workflows = new();
        private List<WorkflowView> WorkflowsSearchs = new();
        bool isEdition = false;
        bool isInsearch = false;

        public MainWindow()
        {
            InitializeComponent();
            InitWorkflows();
        }

        private void InitWorkflows()
        {
            List<Block> macroNotePads = new();
            macroNotePads.Add(new Blocks.B_RunApp("Run Application", "", "", "notepad.exe"));
            macroNotePads.Add(new Blocks.B_Wait("", "", "", 0, 0, 2));
            macroNotePads.Add(new Blocks.B_KeyBoardShortCut("", "", "", "hello world ^s "));
            WorkFlow macroNotePad = new("", "macroNotePads", macroNotePads);

            List<Block> machromes = new();
            machromes.Add(new LaunchBrowserChromeCopy("https://royaleapi.com/player/2GPUV2Y0"));
            machromes.Add(new Blocks.B_Wait("", "", "", 0, 0, 5));
            machromes.Add(new Capture("test", "png", $@"C:\Users\maxim\OneDrive\Bureau", 1));
            WorkFlow machrome = new("", "machrome", machromes);


            List<Block> mailcro = new();
            mailcro.Add(new SendEmail("test", "lpmusardo@gmail.com", "Subject"));
            mailcro.Add(new Blocks.B_Wait("", "", "", 0, 0, 2));
            mailcro.Add(new Recognition($@"C:\Users\maxim\OneDrive\Bureau\gmail.png", debugMode: true));
            mailcro.Add(new ClickG());
            mailcro.Add(new Blocks.B_Wait("", "", "", 0, 0, 2));
            mailcro.Add(new Recognition($@"C:\Users\maxim\OneDrive\Bureau\send.jpeg", debugMode: true));
            mailcro.Add(new ClickG());
            WorkFlow macro2 = new("", "mailcro", mailcro);


            WorkFlow lpmacro = new("", "lpmacro", new List<Block>());



            WorkFlow macro4 = new("", "Test4", new List<Block>());
            WorkFlow macro5 = new("", "Test5", new List<Block>());

            FavWorkflows.Add(new(macroNotePad));
            FavWorkflows.Add(new(machrome));
            FavWorkflows.Add(new(macro2));
            FavWorkflows.Add(new(lpmacro));
            Workflows.Add(new(macroNotePad));
            Workflows.Add(new(machrome));
            Workflows.Add(new(macro2));
            Workflows.Add(new(lpmacro));
            Workflows.Add(new(macro4));
            Workflows.Add(new(macro5));


            foreach (WorkflowView FavWorkflow in FavWorkflows)
            {
                FavWorkflow.Btn_Delete.Visibility = Visibility.Hidden;
                FavWorkflow.Btn_Main.Click += Button_Click_Fav;
                FavWorkflow.Btn_Fav.Click += OnClick_Delete_Fav;
                ListFav.Items.Add(FavWorkflow.Content);
            }
            foreach (WorkflowView Workflow in Workflows)
            {
                Workflow.Btn_Delete.Click += OnClick_Delete;
                Workflow.Btn_Fav.Click += OnClick_Fav;
                Workflow.Btn_Main.Click += Button_Click;
                ListMacro.Items.Add(Workflow.Content);
            }
        }


        private void OnClick_Delete_Fav(object sender, RoutedEventArgs e)
        {
            int currentItemPos = ListFav.Items.IndexOf(((Button)sender).Parent);
            ListFav.Items.RemoveAt(currentItemPos);
            FavWorkflows.RemoveAt(currentItemPos);
        }


        private void OnClick_Delete(object sender, RoutedEventArgs e)
        {
            int currentItemPos = ListMacro.Items.IndexOf(((Button)sender).Parent);

            if (!isInsearch)
            {
                removeWorkflow(Workflows, currentItemPos);
            }
            else
            {
                removeWorkflow(WorkflowsSearchs, currentItemPos);
                WorkflowsSearchs.RemoveAt(currentItemPos);
            }

        }





        private void removeWorkflow(List<WorkflowView> wfs, int index)
        {
            int currentItemPosFav = 0;
            bool test = false;
            while (currentItemPosFav < FavWorkflows.Count)
            {
                if (!wfs[index].CurrentworkFlow.Equals(FavWorkflows[currentItemPosFav].CurrentworkFlow))
                {
                    currentItemPosFav++;
                }
                else
                {
                    test = true;
                    break;
                }

            }
            if (test)
            {
                FavWorkflows.RemoveAt(currentItemPosFav);
                ListFav.Items.RemoveAt(currentItemPosFav);
            }

            ListMacro.Items.RemoveAt(index);
            Workflows.RemoveAt(index);

        }



        private void OnClick_Fav(object sender, RoutedEventArgs e)
        {
            int currentItemPos = ListMacro.Items.IndexOf(((Button)sender).Parent);

            if (!isInsearch)
            {
                AddFav(new WorkflowView(Workflows[currentItemPos].CurrentworkFlow));
            }
            else
            {
                AddFav(new WorkflowView(WorkflowsSearchs[currentItemPos].CurrentworkFlow));
            }

        }

        private void AddFav(WorkflowView newFav)
        {
            WorkFlow wf = newFav.CurrentworkFlow;
            if (FavWorkflows.Count < 5)
            {
                if (!ListContains(FavWorkflows, wf))
                {
                    newFav.Btn_Fav.Click += OnClick_Delete_Fav;
                    newFav.Btn_Main.Click += Button_Click_Fav;
                    newFav.Btn_Delete.Visibility = Visibility.Hidden;
                    ListFav.Items.Add(newFav.Content);
                    FavWorkflows.Add(newFav);
                }

            }


        }

        private bool ListContains(List<WorkflowView> wfls, WorkFlow wf)
        {
            bool result = false;
            foreach (WorkflowView wfItem in wfls)
            {
                if (wfItem.CurrentworkFlow.Equals(wf))
                {
                    return true;
                }

            }

            return result;
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string txt = Search.Text;
            WorkflowsSearchs = new();
            if (txt != "")
            {
                isInsearch = true;
                foreach (WorkflowView m in Workflows)
                {
                    if (m.CurrentworkFlow.workflowName.Contains(txt))
                        WorkflowsSearchs.Add(m);
                }
            }
            else
            {
                isInsearch = false;
                WorkflowsSearchs = Workflows;
            }
            ListMacro.Items.Clear();
            foreach (WorkflowView mac in WorkflowsSearchs)
            {
                ListMacro.Items.Add(mac.Content);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int currentItemPos = ListMacro.Items.IndexOf(((Button)sender).Parent);

            if (isInsearch)
            {
                executeWorkflow((WorkflowsSearchs[currentItemPos].CurrentworkFlow));
            }
            else
            {
                executeWorkflow((Workflows[currentItemPos].CurrentworkFlow));
            }

        }



        private void Button_Click_Fav(object sender, RoutedEventArgs e)
        {
            int currentItemPos = ListFav.Items.IndexOf(((Button)sender).Parent);
            executeWorkflow(FavWorkflows[currentItemPos].CurrentworkFlow);

        }

        private void executeWorkflow(WorkFlow wf)
        {

            if (isEdition)
            {
                EditionWindow editionWindow = new(wf);
                editionWindow.Show();
            }
            else
            {
                foreach (Block m in wf.workflowList)
                {
                    m.Execute();
                }

            }

        }
        private void EditionMode(object sender, RoutedEventArgs e)
        {
            if (isEdition)
            {
                ButtonEdit.Foreground = Brushes.Black;
                isEdition = false;
            }
            else
            {
                ButtonEdit.Foreground = Brushes.Green;
                isEdition = true;
            }
        }
    }

}