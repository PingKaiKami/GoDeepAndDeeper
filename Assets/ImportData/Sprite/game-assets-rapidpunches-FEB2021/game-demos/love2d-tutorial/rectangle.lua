--! file: rectangle.lua
-- Pass Object as first argument.
--Rectangle = Object.extend(Object)
Rectangle = Object:extend()
Rectangle = Shape:extend()

--Lua turns this into: Rectangle.new(self)
function Rectangle:new(x, y, width, height)
    self.test = math.random(1, 1000)
    Rectangle.super.new(self, x, y)
    self.x = x
    self.y = y
    self.width = width
    self.height = height
    self.speed = 100
end

--Lua turns this into: Rectangle.update(self, dt)
function Rectangle:update(dt)
    self.x = self.x + self.speed * dt
end

--Lua turns this into: Rectangle.draw(self)
function Rectangle:draw()
    love.graphics.rectangle("line", self.x, self.y, self.width, self.height)
end