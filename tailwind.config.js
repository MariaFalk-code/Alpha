/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ['./Views/**/*.cshtml',
        './wwwroot/**/*.js'],
  theme: {
      extend: {
          colors: {
              blue: {
                  accent: 'hsl(209, 100.00%, 58.80%)'
              },
          },
          fontFamily: {
              sans: ['Nunito', 'sans-serif'],
              logo: ['Comfortaa', 'cursive'],
          }
},
  },
  plugins: [],
}

