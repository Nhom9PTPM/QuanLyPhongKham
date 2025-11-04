
async function sendMessage() {
  const input = document.getElementById("userInput");
  const chatBox = document.getElementById("chat-box");
  const userMessage = input.value.trim();

  if (!userMessage) return;

  // Hiển thị tin nhắn người dùng
  chatBox.innerHTML += `<p><strong>Bạn:</strong> ${userMessage}</p>`;
  input.value = "";

  // Gửi đến OpenAI GPT
  const response = await fetch("https://api.openai.com/v1/chat/completions", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${API_KEY}`
    },
    body: JSON.stringify({
      model: "gpt-3.5-turbo",
      messages: [{ role: "user", content: `Tôi có triệu chứng sau: ${userMessage}. Bạn có thể giúp tôi phân tích vấn đề y tế không?` }],
      temperature: 0.5
    })
  });

  const data = await response.json();
  const reply = data.choices[0].message.content;

  // Hiển thị phản hồi
  chatBox.innerHTML += `<p><strong>HeartCare:</strong> ${reply}</p>`;
  chatBox.scrollTop = chatBox.scrollHeight;
}
jhsdfjds