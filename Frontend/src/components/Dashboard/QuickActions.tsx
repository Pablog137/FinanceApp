export default function QuickActions() {
  return (
    <div className="col-span-12 py-4">
      <h1 className="text-xl font-bold">Quick actions</h1>
      <div className="flex gap-10 pt-4">
        <div className="p-4 bg-white rounded-md flex flex-col items-center flex-1">
          <i className="fa-solid fa-paper-plane p-2 bg-green-100 rounded-sm text-green-400 text-2xl"></i>
          <h5 className="font-semibold pt-2">Money transfer</h5>
        </div>
        <div className="p-4 bg-white rounded-md flex flex-col items-center flex-1">
          <i className="fa-solid fa-paper-plane p-2 bg-blue-100 rounded-sm text-blue-400 text-2xl"></i>
          <h5 className="font-semibold pt-2">Pay bill</h5>
        </div>
      </div>
    </div>
  );
}
