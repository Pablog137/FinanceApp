export default function Error({ currentError }: { currentError: string }) {
  return (
    <div className="w-full text-red-300 text-sm text-center p-2 mb-5 rounded-md border border-red-900 bg-red-900">
      {currentError}
    </div>
  );
}
