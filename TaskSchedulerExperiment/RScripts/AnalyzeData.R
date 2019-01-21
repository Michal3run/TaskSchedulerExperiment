library(ggplot2)

setwd("E:\\Projects\\TaskSchedulerExperiment\\TaskSchedulerExperiment\\Input")
files = dir()

load_file <- function(file_name) {
  l_data = read.csv(file_name)
  c(name=file_name, data = l_data)
}

data = lapply(files, load_file)

for (list in data) {
  plot=qplot(list$data.CreateTime, breaks = 0:20000 * 50, main = list$name)
  ggsave(plot,file=paste(list$name,".pdf"))
  
}