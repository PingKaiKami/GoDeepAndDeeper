io.stdout:setvbuf("no")
love.graphics.setDefaultFilter("nearest","nearest")
--[[
-- This line is a comment. This is not code.
-- The next line is code:
print(123)
--Output: 123
print(3 + 4)
--Output: 7

--A variable is a word in which you can store a value.
--a variable can be anything you want it to be. Hence the name variable.
a = 20 - 10 --Substraction
b = 20 * 10 --Multiplication
c = 20 / 10 --Division
d = 20 ^ 10 --Exponentiation
print(a + b)
--Output: 8
--Variables are case sensitive.
--Connect strings by using two dots..
name = "Daniel"
age = "25"
text = "Hello, my name is " .. name .. ", and I'm " .. age .. " years old."
print(text)
--Output: "Hello, my name is Daniel, and I'm 25 years old."
--Variables can't begin with number or have special characters.
--coins = coins + 1

function example()
    print("Hey World!")
end

example()

function sayNumber(num)
    print("I like the number " .. num)
end

sayNumber(15)
sayNumber(2)
sayNumber(44)
sayNumber(100)
print(num)
--Output:
--"I like the number 15"
--"I like the number 2"
--"I like the number 44"
--"I like the number 100"
--nil

function giveMeFive()
    return 5
end

e = giveMeFive()
print(e)
--Output: 5

function sum(f, g)
    return f + g
end

print(sum(200, 95))
--Output:
--295

]]

function love.load()
  tick = require "tick"
  Object = require "classic"
  require "shape"
  require "rectangle"
  require "circle"
  require "player"
  require "enemy"
  require "bullet"
  myImage = love.graphics.newImage("8bit-pufferfish-1.png")
  x = 128
  y = 128
  w = 32
  z = 32
  r1 = Rectangle(100, 100, 200, 50)
  r2 = Circle(350, 80, 40) 
  print(r1.test, r2.test)
  player = Player()
  enemy = Enemy()
  listOfBullets = {}

end

function love.update(dt)
  tick.update(dt)
  r1.update(r1, dt)
  r1:update(dt)
  r2:update(dt)
  player:update(dt)
  enemy:update(dt)

    for i,v in ipairs(listOfBullets) do
        v:update(dt)

        --Each bullets checks if there is collision with the enemy
        v:checkCollision(enemy)
    end
end

function love.draw()
  r1.draw(r1)
  r1:draw()
  r2:draw()
  love.graphics.rectangle("line", x, y, 200, 150)
  love.graphics.rectangle("line", w, z, 24, 24)
  love.graphics.draw(myImage, 100, 100, 0, 2, 2)
  player:draw()
  enemy:draw()
  
  for i,v in ipairs(listOfBullets) do
        v:draw()
  end

end

function love.keypressed(key)
    --If backspace is pressed then..
    player:keyPressed(key)
    if key == "backspace" then
        --w and z become a random number between 100 and 500
        w = math.random(100, 500)
        z = math.random(100, 500)
    end

end
--chapter15