import RecentTransferElement from "./RecentTransferElement";

export default function RecentTransfer() {
  return (
    <div className="flex flex-col items-center ">
      <h1 className="text-lg font-semibold pt-16 pb-10 text-start">
        Recent transfers
      </h1>
      <ul className="flex gap-4 ">
        <RecentTransferElement name="John Doe" amount={100} />
        <RecentTransferElement name="Danny" amount={50} />
        <RecentTransferElement name="Peter" amount={20.5} />
        <RecentTransferElement name="Jane" amount={10} />
      </ul>
    </div>
  );
}
