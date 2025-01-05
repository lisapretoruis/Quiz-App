using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Question> Questions;
        private int CurrentQuestionIndex = 0;
        private int Score = 0;

        public class Question
        {
            public string Text { get; set; }
            public List<string> Options { get; set; }
            public int CorrectOptionIndex { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            // Initialize questions
            Questions = new List<Question>
            {
                new Question
                {
                    Text = "What is the capital of France?",
                    Options = new List<string> { "Paris", "London", "Berlin", "Madrid" },
                    CorrectOptionIndex = 0
                },
                new Question
                {
                    Text = "Which planet is known as the Red Planet?",
                    Options = new List<string> { "Earth", "Mars", "Jupiter", "Venus" },
                    CorrectOptionIndex = 1
                },
                new Question
                {
                    Text = "What is 2 + 2?",
                    Options = new List<string> { "3", "4", "5", "6" },
                    CorrectOptionIndex = 1
                }
            };

            // Load the first question
            LoadQuestion();
        }


        private void LoadQuestion()
        {
            var question = Questions[CurrentQuestionIndex];
            QuestionText.Text = question.Text;

            // Clear previous options
            AnswerOptions.Children.Clear();

            // Add buttons for options
            for (int i = 0; i < question.Options.Count; i++)
            {
                var optionButton = new Button
                {
                    Content = question.Options[i],
                    Tag = i,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(5)
                };
                optionButton.Click += OptionButton_Click;
                AnswerOptions.Children.Add(optionButton);
            }
        }

        private void OptionButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedButton = sender as Button;
            int selectedOptionIndex = (int)selectedButton.Tag;

            // Check if the answer is correct
            if (selectedOptionIndex == Questions[CurrentQuestionIndex].CorrectOptionIndex)
            {
                selectedButton.Background = Brushes.LightGreen;
                Score++;
            }
            else
            {
                selectedButton.Background = Brushes.LightCoral;
            }

            // Disable all buttons after answering
            foreach (var child in AnswerOptions.Children)
            {
                if (child is Button button)
                {
                    button.IsEnabled = false;
                }
            }

            // Enable the Next button
            NextButton.IsEnabled = true;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentQuestionIndex++;

            if (CurrentQuestionIndex < Questions.Count)
            {
                LoadQuestion();
                NextButton.IsEnabled = false;
            }
            else
            {
                MessageBox.Show($"Quiz finished! Your score: {Score}/{Questions.Count}", "Quiz Completed");
                Application.Current.Shutdown(); // Exit the app
            }
        }


    }
}