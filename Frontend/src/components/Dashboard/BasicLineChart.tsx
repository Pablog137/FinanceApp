import { LineChart } from "@mui/x-charts/LineChart";

export default function BasicLineChart() {
  return (
    <div className="hidden md:block col-span-12 rounded-md border-2 text-gray-700 bg-gray-100">
      <h1 className="text-xl font-bold p-4 border-b">Analytics</h1>
      <div className="p-4 flex flex-col items-center">
        <div className="flex flex-col self-start">
          <h5 className="font-bold">$10.840,00</h5>
          <p className="text-sm font-thin">Total income</p>
        </div>
        <LineChart
          xAxis={[
            {
              data: [
                "Jan",
                "Feb",
                "Mar",
                "April",
                "May",
                "Jun",
                "Jul",
                "Aug",
                "Sep",
              ],
              scaleType: "band",
            },
          ]}
          series={[
            {
              data: [2, 5.5, 2, 4.5, 8.5, 3, 7.2, 1.5, 4],
            },
          ]}
          width={500}
          height={300}
        />
      </div>
    </div>
  );
}
